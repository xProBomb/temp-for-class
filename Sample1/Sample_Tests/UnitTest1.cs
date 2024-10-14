using NUnit.Framework;
using Sample2.Controllers;
using Sample2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Sample_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatesCorrectNumTeams()
        {
            var teams = new Teams
            {
                TeamSize = 2,
                Names = "Test1\nTest2\nTest3\nTest4\nTest5\nTest6\nTest7\nTest8\nTest9\nTest10"
            };

            var controller = new HomeController(null);
            var result = controller.TeamViewer(teams) as ViewResult;
            var model = result.Model as TeamViewerViewModel;

            Assert.AreEqual(5, model.NumberOfTeams);
        }

        [Test]
        public void EmptyTeams()
        {
            var teams = new Teams
            {
                TeamSize = 2,
                Names = ""
            };

            var controller = new HomeController(null);
            var result = controller.TeamViewer(teams) as ViewResult;
            var model = result.Model as TeamViewerViewModel;

            Assert.AreEqual(0, model.NumberOfTeams);
        }

        [Test]
        public void UnevenTeams()
        {
            var teamsModel = new Teams
            {
                Names = "Test1\nTest2\nTest3\nTest4\nTest5",
                TeamSize = 2
            };

            var controller = new HomeController(null);
            var result = controller.TeamViewer(teamsModel) as ViewResult;
            var viewModel = result.Model as TeamViewerViewModel;

            Assert.AreEqual(3, viewModel.NumberOfTeams);
            Assert.AreEqual(1, viewModel.Teams[2].Members.Count);
        }

        [Test]
        public void InvalidTeamSize()
        {
            var teamsModel = new Teams
            {
                Names = "Test1\nTest2\nTest3\nTest4\nTest5",
                TeamSize = 1
            };

            var controller = new HomeController(null);
            controller.ModelState.AddModelError("TeamSize", "Team size must be between 2 and 10");

            var result = controller.TeamViewer(teamsModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}