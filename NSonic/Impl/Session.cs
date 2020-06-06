﻿using NSonic.Utils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NSonic.Impl
{
    class Session : ISession
    {
        public Session(IClient client)
        {
            this.Client = client;
        }

        public IClient Client { get; }

        public void Dispose()
        {
            //
        }

        public string Read()
        {
            return new StreamReader(this.Client.GetStream()).ReadLine();
        }

        public async Task<string> ReadAsync()
        {
            return await new StreamReader(await this.Client.GetStreamAsync()).ReadLineAsync();
        }

        public void Write(params string[] args)
        {
            var writer = new StreamWriter(this.Client.GetStream());
            writer.WriteLine(this.CreateMessage(args));
            writer.Flush();
        }

        public async Task WriteAsync(params string[] args)
        {
            var writer = new StreamWriter(await this.Client.GetStreamAsync());
            await writer.WriteLineAsync(this.CreateMessage(args));
            await writer.FlushAsync();
        }

        private string CreateMessage(string[] args)
        {
            var message = string.Join(" ", args.Where(a => !string.IsNullOrEmpty(a))).Trim();
            Assert.IsTrue(message.Length <= this.Client.Environment.MaxBufferStringLength, "Message was too long", message);

            return message;
        }
    }
}
