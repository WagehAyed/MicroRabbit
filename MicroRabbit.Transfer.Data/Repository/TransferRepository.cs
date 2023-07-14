using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context;
        public TransferRepository(TransferDbContext context)
        {
            _context=context;
        }

        public int Add(TransferLog transferLog)
        { 
             _context.TransferLog.Add(transferLog);
            return  _context.SaveChanges() ;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _context.TransferLog;
        }
    }
}
