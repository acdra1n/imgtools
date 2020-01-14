using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgtools.IPC
{
    public class IPCServer
    {
        private volatile NamedPipeServerStream npss;
        private Thread ipcThread;
        private bool isRunning = false;
        public string IPCNameExtension { get; set; } = "";

        public IPCServer()
        {
            ipcThread = new Thread(ThreadProc);
        }
        
        void ThreadProc()
        {
            while(isRunning)
            {
                try
                {
                    npss.WaitForConnection();

                    int bytesRead = 0;
                    byte[] buffer = new byte[1024]; //Reads 8k messages
                    List<byte> constructedData = new List<byte>();
                    while ((bytesRead = npss.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        if (bytesRead != buffer.Length)
                        {
                            for (int i = 0; i < bytesRead; i++)
                                constructedData.Add(buffer[i]);
                        }
                        else constructedData.AddRange(buffer);
                    }

                    using (BinaryReader br = new BinaryReader(new MemoryStream(constructedData.ToArray())))
                    {
                        if(br.ReadInt32() != 0x54474923)
                        {
                            Console.WriteLine("Received data does not have a valid signature.");
                            continue;
                        }
                        else if(br.ReadInt32() == 0x54474924)
                        {
                            npss.Disconnect();
                        }
                    }

                }
                catch (Exception)
                {
                    //Console.WriteLine(ex.Message);
                    continue;
                    //TODO Handle errors
                }
            }
        }

        public void StartThreadedProc()
        {
            if (isRunning) return;
            npss = new NamedPipeServerStream("imt-proc" + IPCNameExtension, PipeDirection.InOut);
            isRunning = true;
            ipcThread.Start();
        }
    }
}
