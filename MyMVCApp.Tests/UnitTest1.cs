using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _users;

        public UserControllerTests()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
                new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
            };

            UserController.userlist = _users;
            _controller = new UserController();
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfUsers()
        {
            var result = _controller.Index();

            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<List<User>>(viewResult.Model);
            var model = viewResult.Model as List<User>;
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public void Details_ReturnsAViewResult_WithAUser()
        {
            var result = _controller.Details(1);

            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<User>(viewResult.Model);
            var model = viewResult.Model as User;
            Assert.Equal("Test User 1", model.Name);
        }

        [Fact]
        public void Create_Post_ReturnsARedirectToActionResult()
        {
            var newUser = new User { Id = 4, Name = "Test User 4", Email = "test4@example.com" };
            var result = _controller.Create(newUser);

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal(4, UserController.userlist.Count);
        }

        [Fact]
        public void Edit_Post_ReturnsARedirectToActionResult()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            var result = _controller.Edit(1, updatedUser);

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Updated User", UserController.userlist.Find(u => u.Id == 1).Name);
        }

        [Fact]
        public void Delete_Post_ReturnsARedirectToActionResult()
        {
            var result = _controller.Delete(1, null);

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal(2, UserController.userlist.Count);
        }
    }
}