
using System;
using System.IO.Pipes;

Console.WriteLine("SimpleClient Started!");

Demo1();

// Demo2();
Console.ReadKey();


void Demo1()
{
    //connect
    var pipe = new NamedPipeClientStream(".", "DemoPipe", PipeDirection.InOut);

    Console.WriteLine("SimpleClient waiting for connection!");
    pipe.Connect(); //will return only once a connection is established
    Console.WriteLine("Connected with server");

    //read data
    var dataReceive = pipe.ReadByte();
    Console.WriteLine("Client receive: " + dataReceive);

    //write data
    byte dataSend = 24;
    pipe.WriteByte(dataSend);
    Console.WriteLine("Client send: " + dataSend);

    //close pipe
    pipe.Close();
}

//void Demo2()
//{
//    //connect
//    var pipe = new NamedPipeClientStream(".", "Demo2Pipe", PipeDirection.InOut);

//    Console.WriteLine("SimpleClient waiting for connection!");
//    pipe.Connect(); //will return only once a connection is established
//    Console.WriteLine("Connected with server");

//    //read data

//    //StreamReader can interpret the binary data from pipe as text
//    using StreamReader sr = new StreamReader(pipe);


//    string? msg;
//    while ((msg = sr.ReadLine()) != null)
//    {
//        Console.Write(msg);
//    }

//}