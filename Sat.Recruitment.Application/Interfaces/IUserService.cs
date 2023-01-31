using Sat.Recruitment.Application.DTO;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserService
    {
        public Result CreateUser(User newUser);
    }
}
