using System.Threading;
using System.Threading.Tasks;

namespace PrintApi.Sockets
{
    public interface IPrintTcpClient
    {
        public Task ConnectAsync(string host, int port, CancellationToken cancellationToken);

        public void Close();

        public Task SendAsync(byte[] data, CancellationToken cancellationToken);
    }
}