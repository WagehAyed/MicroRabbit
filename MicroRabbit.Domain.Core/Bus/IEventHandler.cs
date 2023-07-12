using MicroRabbit.Domain.Core.Events;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in Tevent> : IEventHandler
        where Tevent : Event
    {
        Task Handle(Tevent @event);

    }

   public interface IEventHandler
    {

    }
}
