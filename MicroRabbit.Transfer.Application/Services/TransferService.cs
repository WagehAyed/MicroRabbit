using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _repository;
        private readonly IEventBus _bus;

        public TransferService(ITransferRepository repository, IEventBus _bus)
        {
            _repository=repository;
            _bus = _bus;
        }
        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _repository.GetTransferLogs();
        }
    }
}
