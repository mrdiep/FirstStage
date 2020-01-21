using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ICommandDispatcher
    {
        Task<CommandExecutionResult> DispatchAsync(ICommand command);
    }
}
