using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactAppApi.Controllers;
using ContactAppApi.Tests.Db;
using System.Linq;
using ContactAppApi.Edmx;
using System.Web.Http.Results;
using ContactAppApi.Models;

namespace ContactAppApi.Tests.Controllers
{
    /// <summary>
    /// Summary description for NamesControllerTest
    /// </summary>
    [TestClass]
    public class NamesControllerTest
    {
        public NamesController Controller { get; set; }
        public NamesControllerTest()
        {
            Controller = new NamesController(new FakeContext());
        }

        [TestMethod]
        public void GetAllNames()
        {
            // act
            var result = Controller.GetNames().ToList();

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 50);
        }

        [TestMethod]
        public void GetSpecificUser()
        {
            var rand = new Random().Next(2, 48);
            // act
            var result = Controller.GetName(rand) as OkNegotiatedContentResult<Name>;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.SubjectId, rand);
        }

        [TestMethod]
        public void AddUser()
        {
            // arrange
            var newUser = new NamesModel() { SubjectId = 52, FirstName = "52", LastName = "52", Mi = "Mrs", Suffix="Mrs" };
            Controller.PostName(newUser);
            // act
            var result = Controller.GetName(52) as OkNegotiatedContentResult<Name>;
            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.SubjectId, 52);
        }
    }
}
