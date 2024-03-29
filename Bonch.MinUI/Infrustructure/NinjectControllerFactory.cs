﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Bonch.Domain.Abstract;
using Bonch.Domain.Concrete;

namespace Bonch.MinUI.Infrustructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        IKernel _kernel;
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IStatisticDataRepository>().To<StatisticDataRepository>();
            _kernel.Bind<IPersonsRepository>().To<PersonsRepository>();
            _kernel.Bind<IInfraStructureRepository>().To<InfraStructureRepository>();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_kernel.Get(controllerType);
        }
    }
}