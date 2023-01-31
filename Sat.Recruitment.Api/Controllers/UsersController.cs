using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Mapper;
using Sat.Recruitment.Application.DTO;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Validators;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(RequestUser requestUser)
        {
            try { 
                var validator = new RequestUserValidator();
                var errors = validator.Validate(requestUser);

                if (errors.Errors.Any())
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = errors.ToString()
                    };

                var newUser = UserMapper.RequestUserToUser(requestUser);

                return _userService.CreateUser(newUser);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = e.Message
                };
            }
        }
    }
}
