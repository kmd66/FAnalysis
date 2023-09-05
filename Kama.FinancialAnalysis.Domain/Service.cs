//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kama.FinancialAnalysis.Domain
//{
//    class Service
//    {
//        public Service(AppCore.IOC.IContainer container)
//        {
//            _container = container;
//            //_requestInfo = _container.Resolve<Core.IRequestInfo>();
//         //   _logger = _container.TryResolve<Core.IEventLogger>();
//        }

//        protected readonly AppCore.IOC.IContainer _container;
//        //protected readonly Core.IRequestInfo _requestInfo;
//       // protected readonly Core.IEventLogger _logger;

//    }

//    class Service<TDataSource>: Service
//        where TDataSource: Core.DataSource.IDataSource
//    {
//        public Service(AppCore.IOC.IContainer container, TDataSource dataSource)
//            :base(container)
//        {
//            _dataSource = dataSource;
//        }

//        protected readonly TDataSource _dataSource;
//    }
//}
