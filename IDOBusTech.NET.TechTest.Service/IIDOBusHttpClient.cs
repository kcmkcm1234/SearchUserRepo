namespace IDOBusTech.NETTech.Test.Service.Interface
{
    using System.Threading.Tasks;

   public interface IIDOBusHttpClient
   {
        Task<T>  GetAsync<T>(string url);
   }
}
