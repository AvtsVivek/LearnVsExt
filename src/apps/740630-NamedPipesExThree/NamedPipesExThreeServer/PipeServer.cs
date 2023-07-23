using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipesExTwoServer
{
    public class PipeServer
    {
        //Represent server-end of the pipe
        private NamedPipeServerStream _underlyingPipe;

        //pipe send binary data, not text.
        //So we are putting a StreamWriter ..
        //..on top of underlyingPipe for text-to-binary conversion
        private StreamWriter _sw;

        private readonly string _pipeName;

        //ctor
        public PipeServer(string pipeName)
        {
            _pipeName = pipeName;
            InitPipe();
        }

        private async void InitPipe()
        {
            _underlyingPipe = new(_pipeName, PipeDirection.InOut);
            await _underlyingPipe.WaitForConnectionAsync();
            _sw = new StreamWriter(_underlyingPipe);

            //make sure data is sent immediately. No buffering or waiting
            _sw.AutoFlush = true;

        }

        public async void WriteIfConnected(string message)
        {
            try
            {
                if (_underlyingPipe.IsConnected)
                {
                    await _sw.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                //to re-initialize incase needed.
                Dispose();
                InitPipe();

            }
        }

        private void Dispose()
        {
            try
            {
                _sw.Dispose();
                _underlyingPipe.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
