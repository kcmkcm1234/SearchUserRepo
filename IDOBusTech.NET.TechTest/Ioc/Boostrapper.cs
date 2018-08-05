namespace IDOBusTech.NETTech.Test
{
    using StructureMap;
    using IDOBusTech.NETTech.Test.Service.Registry;
    using System.Web.Mvc;

    public static class Boostrapper
    {
        public static IContainer Startup()
        {
           var container=  new Container(x =>
           {            
                x.IncludeRegistry<IDOBusTechServiceRegistry>();                  
           });

           DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

           return container;
        }
    }
}
