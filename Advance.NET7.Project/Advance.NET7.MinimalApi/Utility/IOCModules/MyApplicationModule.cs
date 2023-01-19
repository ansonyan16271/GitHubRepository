using Autofac;
using System.Reflection;

namespace Advance.NET7.MinimalApi.Utility.IOCModules
{
    public class MyApplicationModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly interfaceAssembly = Assembly.Load("Advance.NET7.MinimalApi.Interfaces");
            Assembly serviceAssembly = Assembly.Load("Advance.NET7.MinimalApi.Services");
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }
    }
}
