using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace PrintApi.Sockets
{
    public class PrintClientInitializer : IHostedService
    {
        private readonly IPrintTcpClient printTcpClient;

        public PrintClientInitializer(IPrintTcpClient printTcpClient)
        {
            this.printTcpClient = printTcpClient ?? throw new ArgumentNullException(nameof(printTcpClient));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.printTcpClient.ConnectAsync("localhost", 13000, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Slight comment here. In general, client is IDisposable, so IServicesCollection will anyway dispose it.
            // But prefer to be explicit. Once this close is called - dispose will start. And tcpclient anyway has dispose with correct checks for disposing....
            this.printTcpClient.Close();
            return Task.CompletedTask;
        }
    }
}