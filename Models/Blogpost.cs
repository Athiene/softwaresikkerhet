using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;
using NuGet.Packaging.Signing;

namespace website.Models;

public class Blogpost
{
    public Blogpost() {}

    [Required]
    public int Id { get; set; }

    [Required]
    [DisplayName("Title")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    [DisplayName("Summary")]
    public string Summary { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    [DisplayName("Content")]
    public string Content { get; set; } = string.Empty;

    [Required]
    [DisplayName("Time")]
    public string Time { get; set; }

    //Foreign Key || Navigation Properties
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}