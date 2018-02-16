using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactAppApi.Controllers;
using ContactAppApi.Tests.Db;
using System.Linq;
using ContactAppApi.Edmx;
using System.Web.Http.Results;

namespace ContactAppApi.Tests.Controllers
{
    /// <summary>
    /// Summary description for NamesControllerTest
    /// </summary>
    [TestClass]
    public class AddressControllerTest
    {
        public AddressesController Controller { get; set; }
        public AddressControllerTest()
        {
            Controller = new AddressesController(new FakeContext());
        }

        [TestMethod]
        public void GetAllAddresses()
        {
            // act
            var result = Controller.GetAddresses().ToList();
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 25);
        }

        [TestMethod]
        public void GetSpecificUser()
        {
            var rand = new Random().Next(2, 23);
            // act
            var result = Controller.GetAddress(rand) as OkNegotiatedContentResult<Address>;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.AddressId, rand);
        }

        [TestMethod]
        public void AddUser()
        {
            // arrange
            var add = new Address() { AddressId = 52, Street = "52", StreetType = "52", State = "MICHIGAN",  City = "Something" };
            Controller.PostAddress(add);
            // act
            var result = Controller.GetAddress(23) as OkNegotiatedContentResult<Address>;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.AddressId, 23);
        }
    }
}
