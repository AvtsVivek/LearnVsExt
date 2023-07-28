using System.IO.Pipes;

namespace NamedPipesExOneServer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("[SERVER] Worker running at: {time}", DateTimeOffset.Now);

            await using var pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.Out);
            _logger.LogInformation("[SERVER] NamedPipeServerStream object created.");

            // Wait for a client to connect
            _logger.LogInformation("[SERVER] Waiting for client connection...");
            await pipeServer.WaitForConnectionAsync();

            _logger.LogInformation("[SERVER] Client connected.");
            try
            {
                // Read user input and send that to the client process.
                await using var sw = new StreamWriter(pipeServer);
                sw.AutoFlush = true;
                _logger.LogInformation("[SERVER] Enter text: ");
                sw.WriteLine(Console.ReadLine());
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                _logger.LogInformation("[SERVER] ERROR: {0}", e.Message);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}