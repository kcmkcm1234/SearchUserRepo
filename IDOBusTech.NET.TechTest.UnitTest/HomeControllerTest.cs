namespace IDOBusTech.NET.TechTest.UnitTest
{
    using Controllers;
    using FluentAssertions;
    using IDOBusTech.NETTech.Test.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using NETTech.Test;
    using NETTech.Test.Service;
    using NETTech.Test.ViewModel;
    using StructureMap;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    [TestClass]
    public class HomeControllerTest
    {
        private const string testUserUrl = "https://api.github.com/users/robconery";
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = Boostrapper.Startup();
        }

        [TestMethod]
        public void Index_ShouldReturnRequestModel()
        {
            var controller = PrepareHomeController();
            ViewResult result = controller.Index() as ViewResult;
            var target = result.Model as RequestModel;

            target.Should().NotBeNull();
        }

        [TestMethod]
        public async Task FatchProfile_ShouldRedirectToRepoController()
        {
            var controller = PrepareHomeController();
           
            var result = await controller.Search(new RequestModel { UserName = "test" });
            var redirectResult = result.As<RedirectToRouteResult>();

            var expectedRedirectValues = new RouteValueDictionary
            {               
                { "action", "Entry" },
                { "controller", "Repo" }
            };

            redirectResult.RouteValues.Should().BeEquivalentTo(expectedRedirectValues);
        }


        #region private

        private HomeController PrepareHomeController()
        {
            var iDOBusHttpClient = new Mock<IDOBusHttpClient>();

            iDOBusHttpClient.Setup(o =>o.GetAsync<Owner>(It.IsAny<string>())).ReturnsAsync((new Owner() { login = "test" }));

            return new HomeController(iDOBusHttpClient.Object);
        }


        #endregion
    }
}
