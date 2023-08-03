using NamedPipesExTwoServer;
using System.IO.Pipes;


namespace NamedPipesExOneServer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly NamedPipeServerStream _pipe;

    //demo-2
    // private readonly PipeServer _pipeServer;


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;

        //create pipe
        _pipe = new NamedPipeServerStream("DemoPipe", PipeDirection.InOut,
            1);

        //demo-2
        // _pipeServer = new PipeServer("Demo2Pipe");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Server is started!");

        await Demo1(stoppingToken);

        //demo-2
        // await Demo2();
        Console.ReadKey();
    }

    private async Task Demo1(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Server-Demo1!");

        //wait until client connects
        await _pipe.WaitForConnectionAsync(stoppingToken);
        _logger.LogInformation("Client is connected");


        //write and read data
        try
        {
            //write data
            byte dataSend = 53;
            _pipe.WriteByte(dataSend);
            _logger.LogInformation($"Server send: {dataSend}");

            //read data
            var dataReceive = _pipe.ReadByte();
            _logger.LogInformation($"Server receive: {dataReceive}");
        }
        //catch exception on broken or disconnected pipe
        catch (IOException exception)
        {
            _logger.LogError("Error: {0}", exception.Message);
        }

        //close pipe
        _pipe.Close();
    }

    //private async Task Demo2()
    //{
    //    _logger.LogInformation("Executing Server-Demo2!");
    //    _pipeServer.WriteIfConnected($"Msg from server UTC: {DateTime.UtcNow}");
    //}
}
