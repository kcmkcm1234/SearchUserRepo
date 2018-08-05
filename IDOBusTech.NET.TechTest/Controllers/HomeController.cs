
namespace IDOBusTech.NET.TechTest.Controllers
{
    using IDOBusTech.NET.TechTest.Filter;
    using IDOBusTech.NETTech.Test.Model;
    using IDOBusTech.NETTech.Test.Service.Interface;
    using NETTech.Test.Mapper;
    using NETTech.Test.ViewModel;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [ErrorFilter]
    public class HomeController : Controller
    {
        private readonly IIDOBusHttpClient _iDOBusHttpClient;
        public HomeController(IIDOBusHttpClient iDOBusHttpClient)
        {
            _iDOBusHttpClient = iDOBusHttpClient;
        }
        public ActionResult Index()
        {    
            return View(new RequestModel());
        }
       
        public async Task<ActionResult> Search(RequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("UserName", "UserName is Invalid");
                return View(requestModel);
            }

            var owner = await _iDOBusHttpClient.GetAsync<Owner>($"https://api.github.com/users/{requestModel.UserName}");

            if (owner == null)
            {
                return RedirectToAction("index", "error");
            }

            TempData["OwnerModel"] = Map.From(owner);
            TempData.Keep();

            return RedirectToAction("Entry", "Repo");
        }
       
    }
}