﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace website.Models;


public class ApplicationUser : IdentityUser
{
    
    public ApplicationUser() {}

    public string Nickname { get; set; }
}