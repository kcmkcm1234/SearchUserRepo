namespace IDOBusTech.NET.TechTest.UnitTest
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IDOBusTech.NETTech.Test.Model;
    using NETTech.Test.Service;
    using StructureMap;
    using NETTech.Test;
    using System.Net.Http;

    [TestClass]
    public class IntegrationTests
    {
        private const string testUserUrl = "https://api.github.com/users/robconery";
        private const string testUserReposUrl = "https://api.github.com/users/robconery/repos";
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            _container = Boostrapper.Startup();
        }

        [TestMethod]
        public void GetAsync_ShouldNotThrowException()
        {
            var iDOBusHttpClient = _container.GetInstance<IDOBusHttpClient>();
            Func<Task> action = async () => { var _ = await iDOBusHttpClient.GetAsync<Owner>(testUserUrl); };
            action.Should().NotThrow();
        }

        [TestMethod]
        public async Task GetAsync_UserProfile_ShouldNotBeNull()
        {
            var iDOBusHttpClient = _container.GetInstance<IDOBusHttpClient>();

            var test = await iDOBusHttpClient.GetAsync<Owner>(testUserUrl);
            test.Should().NotBeNull();           
        }

        [TestMethod]
        public async Task GetAsync_UserRepos_ShouldNotBeNull()
        {
            var iDOBusHttpClient = _container.GetInstance<IDOBusHttpClient>();
            var test = await iDOBusHttpClient.GetAsync<List<Repo>>(testUserReposUrl);
            test.Should().NotBeNull();
        }

        [TestMethod]
        public void GetAsync_ShouldThrowHttpRequestException()
        {
            var iDOBusHttpClient = _container.GetInstance<IDOBusHttpClient>();
            Func<Task> action = async () => { var _ = await iDOBusHttpClient.GetAsync<Owner>(testUserUrl + "__"); };
            action.Should().Throw<HttpRequestException>();
        }
    }
}
