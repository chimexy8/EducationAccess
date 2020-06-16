using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashMoneyApi.Validators
{
    public class PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var userr = await manager.GetUserNameAsync(user);
            if (userr == password)
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain username" });
            if(password.ToLower().Contains("password"))
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain the word \"Password\"" });
            return IdentityResult.Success;
        }
    }
}
