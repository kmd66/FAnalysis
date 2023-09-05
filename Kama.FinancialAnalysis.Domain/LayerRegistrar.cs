using Kama.AppCore.IOC;
using System;
using DMN = Kama.FinancialAnalysis.Domain;

[assembly: Registrar(typeof(DMN.LayerRegistrar), Order = 2)]
namespace Kama.FinancialAnalysis.Domain
{
    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            //ASM asmInterfaces = ASM.GetAssembly(typeof(svc.IService)),
            //    asmClasses = ASM.GetAssembly(this.GetType());

            //container.RegisterFromAssembly(
            //    servicesAssembly: asmInterfaces,
            //    implementationsAssembly: asmClasses,
            //    isService: t => t.IsInterface && !t.IsClass && typeof(svc.IService).IsAssignableFrom(t),
            //    isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(DMN.Service))
            //    );

            //RegisterTimers(container);
        }
    }
}
