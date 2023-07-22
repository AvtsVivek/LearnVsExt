using System.IO.Pipes;

namespace NamedPipesExOneClient;

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
            _logger.LogInformation("[CLIENT] Worker running at: {time}", DateTimeOffset.Now);

            await using var pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.In);

            // Connect to the pipe or wait until the pipe is available.
            _logger.LogInformation("[CLIENT] Attempting to connect to pipe...");
            pipeClient.Connect();

            _logger.LogInformation("[CLIENT] Connected to pipe.");

            using var sr = new StreamReader(pipeClient);
            string? temp;
            while ((temp = sr.ReadLine()) != null)
            {
                _logger.LogInformation("[CLIENT] Received from server: {0}", temp);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}