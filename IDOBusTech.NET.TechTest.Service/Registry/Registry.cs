namespace IDOBusTech.NETTech.Test.Service.Registry
{
    using StructureMap;
    using StructureMap.Configuration.DSL;
    using IDOBusTech.NETTech.Test.Service.Interface;

    public class IDOBusTechServiceRegistry : Registry
    {
        public IDOBusTechServiceRegistry()
        {            
            For<IIDOBusHttpClient>().Use<IDOBusHttpClient> ();
        }
    }
}
