using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChartCareMVC.Models;

namespace ChartCareMVC.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CompanyUser class
public class CompanyUser : IdentityUser
{
    public int CompanyID { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public required Company Company { get; set; }
}

