﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.DAL.Models
{
    public class ApplicationUser :IdentityUser
    {

        public string FristName { get; set; }
        public string LastName { get; set; }

        public bool IsAgree { get; set; }



    }
}