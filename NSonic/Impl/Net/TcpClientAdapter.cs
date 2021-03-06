﻿using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NSonic.Impl.Net
{
    class TcpClientAdapter : ITcpClient
    {
        private TcpClient client;

        public bool Connected => this.client?.Connected ?? false;

        public SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);
        public StreamReader StreamReader { get; private set; }
        public StreamWriter StreamWriter { get; private set; }

        public virtual void Connect(string hostname, int port)
        {
            // Underlying stream will be disposed here which means we do not have to
            // dispose the reader/writer.
            this.client?.Dispose();

            this.client = new TcpClient
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            this.client.Connect(hostname, port);

            this.StreamWriter = new StreamWriter(this.client.GetStream());
            this.StreamReader = new StreamReader(this.client.GetStream());
        }

        public virtual async Task ConnectAsync(string hostname, int port)
        {
            this.client?.Dispose();
            this.client = new TcpClient
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            await this.client.ConnectAsync(hostname, port);
            
            this.StreamWriter = new StreamWriter(this.client.GetStream());
            this.StreamReader = new StreamReader(this.client.GetStream());
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }
    }
}
