using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application;
using Sat.Recruitment.Application.DTO;
using Sat.Recruitment.Application.Interfaces;
using System.IO;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private Mock<IReadService> _mockReadService = new Mock<IReadService>();
        public UnitTest1()
        {
        }
        [Fact]
        public void Test1()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("Jonah,adada@gmail.com,Av. Juan G,+349 999858,Normal,124");

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));
            _mockReadService.Setup(x => x.ReadUsersFromFile()).Returns(new StreamReader(ms));
            _mockReadService.Setup(X => X.WriteUserInFile(It.IsAny<string>()));

            IUserService userService = new UserService(_mockReadService.Object);

            var userController = new UsersController(userService);

            var user = new RequestUser()
            {
                name = "Mike",
                email = "mike@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };

            var result = userController.CreateUser(user).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("Agustina,Agustina@gmail.com,Av. Juan G,+349 1122354215,Normal,124");

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));
            _mockReadService.Setup(x => x.ReadUsersFromFile()).Returns(new StreamReader(ms));
            _mockReadService.Setup(X => X.WriteUserInFile(It.IsAny<string>()));

            IUserService userService = new UserService(_mockReadService.Object);

            var userController = new UsersController(userService);
            var user = new RequestUser()
            {
                name = "Agustina",
                email = "Agustina@gmail.com",
                address = "Av. Juan G",
                phone = "+349 1122354215",
                userType = "Normal",
                money = "124"
            };
            var result = userController.CreateUser(user).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
