
using System;
using System.IO;
using System.IO.Pipes;

Console.WriteLine("SimpleClient Started!");

Demo2();
Console.ReadKey();



void Demo2()
{
    //connect
    var pipe = new NamedPipeClientStream(".", "Demo2Pipe", PipeDirection.InOut);

    Console.WriteLine("SimpleClient waiting for connection!");
    pipe.Connect(); //will return only once a connection is established
    Console.WriteLine("Connected with server");

    //read data

    //StreamReader can interpret the binary data from pipe as text
    using StreamReader sr = new StreamReader(pipe);


    string? msg;
    while ((msg = sr.ReadLine()) != null)
    {
        Console.Write(msg);
    }

}