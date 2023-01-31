using Sat.Recruitment.Application.DTO;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Api.Mapper
{
    public static class UserMapper
    {
        public static User RequestUserToUser(RequestUser request)
        {
            User user = new User
            {
                Name = request.name,
                Email = request.email,
                Address = request.address,
                Phone = request.phone,
                UserType = request.userType,
                Money = decimal.Parse(request.money)
            };
            return user;
        }
    }
}
