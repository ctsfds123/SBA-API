

using ProjecManagement.ErrorHandler;
using ProjecManagement.Repositories;

namespace ProjecManagement.Services.App_Start
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.AddRegistry<ServicesRegistry>();
                x.For<IErrorLog>().Use<ErrorLogDetails>().Singleton();
            });

            var log = ObjectFactory.Container.WhatDoIHave();
        }
    }
    public class ServicesRegistry : StructureMap.Registry
    {
        public ServicesRegistry()
        {
            Scan(x =>
            {
                x.Assembly("ProjectManagement.BusinessLayer");
                x.Assembly("ProjectManagement.ErrorHandler"); 
                x.Assembly("ProjectManagement.Repositories");
                x.Assembly("ProjectManagementServices");
                x.WithDefaultConventions();
            });

            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}