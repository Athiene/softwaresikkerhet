using website.Data;
using website.Models;
using Microsoft.AspNetCore.Identity;

namespace wensite.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        // Delete the database before we initialize it. This is common to do during development.
        db.Database.EnsureDeleted();

        // Recreate the database and tables according to our models
        db.Database.EnsureCreated();
        
        // Create roles
        var adminRole = new IdentityRole("Admin");
        rm.CreateAsync(adminRole).Wait();
        
        var normalRegUser = new IdentityRole("User");
        rm.CreateAsync(normalRegUser).Wait();
        
        // Create users
        var admin = new ApplicationUser { UserName = "admin@uia.no", Email = "admin@uia.no", Nickname = "Per Arne"};
        um.CreateAsync(admin, "Password1.").Wait();
        um.AddToRoleAsync(admin, "Admin").Wait();
        
        var user = new ApplicationUser { UserName = "user@uia.no", Email = "user@uia.no", EmailConfirmed = true, Nickname = "Arvin"};
        um.CreateAsync(user, "Password1.").Wait();
        
        // Creating dummy data
        var blogposts = new[]
        {
            new Blogpost { Title = "Hello World!", Content = "Nice content about starting in the programming world.", Summary = "Hello, this is an introductory post to programming.", Time = DateTime.Now.Date.ToString("dd.MM.yyyy"), ApplicationUser = user },
            new Blogpost { Title = "Exploring the Universe of AI", Content = "Diving deep into the fascinating world of Artificial Intelligence.", Summary = "Let's explore the wonders of AI and its applications.", Time = DateTime.Now.Date.ToString("dd.MM.yyyy"), ApplicationUser = admin },
            new Blogpost { Title = "Cooking Adventures", Content = "Sharing some delicious recipes and cooking tips.", Summary = "Join me on a culinary journey filled with flavors and techniques.", Time = DateTime.Now.Date.ToString("dd.MM.yyyy"), ApplicationUser = user },
            new Blogpost { Title = "Fitness Fundamentals", Content = "Tips and tricks for a healthy lifestyle and effective workouts.", Summary = "Unlock the secrets to a fit and active life with these fundamentals.", Time = DateTime.Now.Date.ToString("dd.MM.yyyy"), ApplicationUser = admin },
            // Add more dummy data as needed
        };

        db.AddRange(blogposts);
        
        db.SaveChanges(); // Finally save changes
    }
    
    
    
    public static void Initialize(ApplicationDbContext db)
    {
        throw new NotImplementedException();
    }

    public static void Initialize(object db, object um, object rm)
    {
        throw new NotImplementedException();
    }
}