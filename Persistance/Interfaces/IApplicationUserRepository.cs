﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetApplicationUser(ApplicationUser applicationUser);
        Task<ApplicationUser> GetApplicationUserByUsername(string username);
        bool AuthenticateViaLDAP(string username, string password);

        
    }
}
