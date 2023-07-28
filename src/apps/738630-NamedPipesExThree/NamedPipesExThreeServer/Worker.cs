using NamedPipesExTwoServer;
using System.IO.Pipes;


namespace NamedPipesExOneServer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly NamedPipeServerStream _pipe;

    //demo-2
    private readonly PipeServer _pipeServer;


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

        //create pipe
        _pipe = new NamedPipeServerStream("DemoPipe", PipeDirection.InOut,
            1);

        //demo-2
        _pipeServer = new PipeServer("Demo2Pipe");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Server is started!");

        await Demo2();
        Console.ReadKey();
    }



    private async Task Demo2()
    {
        _logger.LogInformation("Executing Server-Demo2!");
        _pipeServer.WriteIfConnected($"Msg from server UTC: {DateTime.UtcNow}");
    }
}
