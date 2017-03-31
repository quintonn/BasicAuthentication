using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthentication.Users
{
    public interface ICoreIdentityUser : IUser<string>
    {
        
    }
}
