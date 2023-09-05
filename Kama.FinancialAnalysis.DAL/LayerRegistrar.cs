using Kama.AppCore.IOC;
using System;
using DAL =  Kama.FinancialAnalysis.DAL;

[assembly: Registrar(typeof(DAL.LayerRegistrar))]
namespace  Kama.FinancialAnalysis.DAL
{
    using ASM = System.Reflection.Assembly;

    class LayerRegistrar : IRegistrar
    {
        readonly Guid _layerID = Guid.NewGuid();

        public Guid ID => _layerID;

        public void Start(IContainer container)
        {
            //    ASM asmInterfaces = ASM.GetAssembly(typeof(ds.IDataSource)),
            //        asmClasses = ASM.GetAssembly(this.GetType());

            //    container.RegisterFromAssembly(
            //        servicesAssembly: asmInterfaces,
            //        implementationsAssembly: asmClasses,
            //        isService: t => t.IsInterface && !t.IsClass && typeof(ds.IDataSource).IsAssignableFrom(t),
            //        isServiceImplementation: t => !t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(DAL.DataSource))
            //        );
        }
    }
}
