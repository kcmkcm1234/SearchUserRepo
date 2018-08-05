
namespace IDOBusTech.NET.TechTest.Controllers
{
    using IDOBusTech.NETTech.Test.Model;
    using IDOBusTech.NETTech.Test.Service.Interface;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using NETTech.Test.Mapper;
    using NETTech.Test.ViewModel;

    public class RepoController : Controller
    {
        private readonly IIDOBusHttpClient _iDOBusHttpClient;
        public RepoController(IIDOBusHttpClient iDOBusHttpClient)
        {
            _iDOBusHttpClient = iDOBusHttpClient;
        }
                
        public async Task<ActionResult> Entry()
        {
            var ownerModel = TempData["OwnerModel"] as OwnerModel;   
            
            if(ownerModel != null)
            {
                return View(await PrepareRepo(ownerModel));
            }

            return RedirectToAction("index", "error");         
        }

        #region private

        private async Task<RepoAdapter> PrepareRepo(OwnerModel owner)
        {
            var repos = await _iDOBusHttpClient.GetAsync<List<Repo>>(owner.ReposUrl);

            var repoAdapter = new RepoAdapter
            {
                Owner = owner,
                Repos = Map.From(repos)
            };

            return repoAdapter;
        }

        #endregion     
       
    }
}