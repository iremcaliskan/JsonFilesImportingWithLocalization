using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services); // Loads the general dependencies known as service collection to the system 
    }
}