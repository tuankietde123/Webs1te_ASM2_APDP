using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Webs1te.Pages.Account;

namespace Webs1te.Tests
{
    public class LoginTests
    {
        [Test]
        public void Login_Successful()
        {
            // Arrange
            var logger = new LoggerFactory().CreateLogger<LoginModel>();
            var pageModel = new LoginModel(logger);
            var input = new LoginModel.InputModel
            {
                Username = "validuser",
                Password = "validpassword"
            };
            pageModel.Input = input;

            // Act
            var result = pageModel.OnPostAsync().Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result is RedirectToPageResult);
            var redirectResult = (RedirectToPageResult)result;
            Assert.AreEqual("/Clients/Index", redirectResult.PageName);
        }

        [Test]
        public void Login_Failed()
        {
            // Arrange
            var logger = new LoggerFactory().CreateLogger<LoginModel>();
            var pageModel = new LoginModel(logger);
            var input = new LoginModel.InputModel
            {
                Username = "invaliduser",
                Password = "invalidpassword"
            };
            pageModel.Input = input;

            // Act
            var result = pageModel.OnPostAsync().Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsTrue(result is PageResult);
            var pageResult = (PageResult)result;
            Assert.AreEqual(string.Empty, pageResult.ViewData.ModelState[string.Empty].Errors[0].ErrorMessage);
        }
    }

    internal class PageResult
    {
    }

    internal class RedirectToPageResult
    {
        internal object? PageName;
    }

    internal class LoginModel
    {
        public LoginModel(ILogger<LoginModel> logger)
        {
            Logger = logger;
        }

        public ILogger<LoginModel> Logger { get; }
        public InputModel Input { get; internal set; }

        internal object OnPostAsync()
        {
            throw new NotImplementedException();
        }

        internal class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}