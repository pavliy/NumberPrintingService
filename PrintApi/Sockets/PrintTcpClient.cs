using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PrintApi.Sockets
{
    public class PrintTcpClient : IPrintTcpClient, IDisposable
    {
        private readonly TcpClient client = new TcpClient();

        private bool connectWasCalled;

        public void Dispose()
        {
            this.client?.Close();
            this.client?.Dispose();
        }

        public async Task ConnectAsync(string host, int port, CancellationToken cancellationToken)
        {
            if (this.connectWasCalled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            cancellationToken.ThrowIfCancellationRequested();
            await this.client.ConnectAsync(host, port);
            this.connectWasCalled = true;
        }

        public void Close()
        {
            this.client.Close();
        }

        public async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            if (!this.connectWasCalled)
            {
                throw new InvalidOperationException("You should call connect first to work with this client");
            }

            NetworkStream networkStream = this.client.GetStream();
            await networkStream.WriteAsync(data);
        }
    }
}