using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChartCareMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChartCareMVC.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CompanyUser class
public class CompanyUser : IdentityUser
{
    [Required]
    public int CompanyID { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }

    [ForeignKey(nameof(CompanyID))]
    public virtual Company Company { get; set; } = null!;
}

