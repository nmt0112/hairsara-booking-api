using Microsoft.AspNetCore.Identity;
using Storage.Models.Authentication.SignUp;
using Storage.Models.Authentication;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.ServiceUser
{
    public interface IUser
    {
        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);

        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, IdentityUser user);
    }
}
