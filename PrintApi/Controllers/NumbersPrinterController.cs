using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PrintApi.Sockets;

namespace PrintApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersPrinterController
    {
        private readonly IPrintTcpClient printClient;

        public NumbersPrinterController(IPrintTcpClient printClient)
        {
            this.printClient = printClient ?? throw new ArgumentNullException(nameof(printClient));
        }

        [HttpPost]
        public async Task<ActionResult> Post(CancellationToken cancellationToken, double number)
        {
            byte[] data = Encoding.ASCII.GetBytes(number.ToString(CultureInfo.InvariantCulture));
            await this.printClient.SendAsync(data, cancellationToken);

            return new StatusCodeResult(204);
        }
    }
}