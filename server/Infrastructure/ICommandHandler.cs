using System.Data;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ICommandHandler<C> where C : ICommand
    {
        Task HandlerAsync(C command);
    }
}
