using AspNetCoreAuthWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthWebApp.Repositories
{
    public interface ILoginRepository
    {
        LoginDto GetUser(Login user);
    }
}
