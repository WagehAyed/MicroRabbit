using MicroRabbit.Transfer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Data.Repository
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        int Add(TransferLog transferLog);
    }
}
