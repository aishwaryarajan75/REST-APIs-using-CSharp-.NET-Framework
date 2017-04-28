using System;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Net;
using MitchellCodingChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitchellCodingChallenge.Controllers;

namespace MitchellCodingChallenge.Tests
{
    [TestClass]
    public class TestVehicleController
    {

        class MockVehiclePersistent : VehiclePersistenceInterface
        {
            public bool deleteVehicle(int id)
            {
                return true;
            }

            public List<Vehicle> getAllVehicles()
            {
                var testVehicles = new List<Vehicle>();
                testVehicles.Add(new Vehicle { Id = 1, Year = 2011, Make = "Ferrari1", Model = "XL" });
                testVehicles.Add(new Vehicle { Id = 2, Year = 2012, Make = "Ferrari2", Model = "XXL" });
                testVehicles.Add(new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" });
                testVehicles.Add(new Vehicle { Id = 4, Year = 2014, Make = "Ferrari4", Model = "XXXXL" });
                return testVehicles;
            }

            public Vehicle getVehicle(int id)
            {

              Vehicle v = new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" };
              return v;
            }

            public int saveVehicle(Vehicle vehicleToSave)
            {
                return vehicleToSave.Id;
            }

            public bool updateVehicle(int id, Vehicle vehicleToUpdate)
            {
                return true;
            }
        }
        [TestMethod]
        public void GetVehicle_ShouldReturnCorrectVehicle()
        {
            Vehicle v = new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" };
            var controller = new VehicleController(new MockVehiclePersistent());
            var result = controller.Get(3);
            Assert.IsNotNull(result);
            Assert.AreEqual(v.Id, result.Id);
        }

        [TestMethod]
        public void GetAllVehicles_ShouldReturnAllVehicles()
        {
            var testVehicles = GetAllVehicles();
            var controller = new VehicleController(new MockVehiclePersistent());
            var result = controller.Get() as List<Vehicle>;
            Assert.AreEqual(testVehicles.Count, result.Count);
        }
        [TestMethod]
        public void SaveVehicle_ShouldSaveVehicleInDatabase()
        {
            Vehicle v = new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" };
            var controller = new VehicleController(new MockVehiclePersistent());
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            controller.Request.RequestUri = new Uri("http://test.com");
            var result = controller.Post(v);
            Assert.AreEqual(new Uri("http://test.com/" + "vehicle/" + v.Id), result.Headers.Location);
        }

        [TestMethod]
        public void UpdateVehicle_ShouldReturnUpdatedVehicle()
        {
            Vehicle v = new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" };
            var controller = new VehicleController(new MockVehiclePersistent());
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            var result = controller.Put(3, v);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void DeleteVehicle_ShouldDeleteVehicleInDatabase()
        {
            Vehicle v = new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" };
            var controller = new VehicleController(new MockVehiclePersistent());
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            var result = controller.Delete(3);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        public List<Vehicle> GetAllVehicles()
        {
            var testVehicles = new List<Vehicle>();
            testVehicles.Add(new Vehicle { Id = 1, Year = 2011, Make = "Ferrari1", Model = "XL" });
            testVehicles.Add(new Vehicle { Id = 2, Year = 2012, Make = "Ferrari2", Model = "XXL" });
            testVehicles.Add(new Vehicle { Id = 3, Year = 2013, Make = "Ferrari3", Model = "XXXL" });
            testVehicles.Add(new Vehicle { Id = 4, Year = 2014, Make = "Ferrari4", Model = "XXXXL" });
            return testVehicles;
        }

    }
}
