using Sat.Recruitment.Application.DTO;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sat.Recruitment.Application
{
    public class UserService : IUserService
    {
        private IReadService _readService;
        private readonly List<User> _users = new List<User>();
        public UserService(IReadService readService)
        {
            _readService = readService;
        }

        public Result CreateUser(User newUser)
        {
            bool moreThan100 = newUser.Money > 100;
            decimal percentage = 0;
            switch (newUser.UserType)
            {
                case "Normal":
                    if (moreThan100)
                        percentage = Convert.ToDecimal(0.12);
                    else
                        percentage = newUser.Money > 10 ? Convert.ToDecimal(0.8) : 0;
                    break;
                case "SuperUser":
                    percentage = moreThan100 ? Convert.ToDecimal(0.20) : 0;
                    break;
                case "Premium":
                    percentage = moreThan100 ? 2 : 0;
                    break;
                default:
                    break;
            }
            newUser.Money = newUser.Money + (newUser.Money * percentage);

            var reader = _readService.ReadUsersFromFile();

            newUser.Email = NormalizeEmail(newUser.Email);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();

            bool isDuplicated = _users.Where(x => x.Email.Equals(newUser.Email) ||
                                x.Phone.Equals(newUser.Phone) ||
                                (x.Name.Equals(newUser.Name) && x.Address.Equals(newUser.Address))).Any();

            if (!isDuplicated)
            {
                var stringUser = string.Join(',', newUser.Name,newUser.Email,newUser.Phone,newUser.Address,newUser.UserType,newUser.Money);
                _readService.WriteUserInFile(stringUser);
                Debug.WriteLine("User created");

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User created"
                };
            }
            else
            {
                Debug.WriteLine("The user is duplicated");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }
    }
}
