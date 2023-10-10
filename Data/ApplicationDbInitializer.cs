using assignment_4.Models;
using Microsoft.AspNetCore.Identity;

namespace assignment_4.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        // Delete the database before we initialize it. This is common to do during development.
        db.Database.EnsureDeleted();

        // Recreate the database and tables according to our models
        db.Database.EnsureCreated();

        
        // Add test data to simplify debugging and testing
        /*
        var blogposts = new[]
        {
            new Blogpost( "Dbd", "lsdnciojcidsjcjocidsjicjdsiocjdsjjdosijcodsjciodsjicdsjodcjs", "iusdchdiucdchsidchudshcdsuicu"),
        };

        db.Blogposts.AddRange(blogposts);
        */
        
        // Create roles
        var adminRole = new IdentityRole("Admin");
        rm.CreateAsync(adminRole).Wait();
        
        var normalRegUser = new IdentityRole("User");
        rm.CreateAsync(normalRegUser).Wait();
        
        // Create users
        var admin = new ApplicationUser { UserName = "admin@uia.no", Email = "admin@uia.no", Nickname = "Auby"};
        um.CreateAsync(admin, "Password1.").Wait();
        um.AddToRoleAsync(admin, "Admin").Wait();

        var user = new ApplicationUser { UserName = "user@uia.no", Email = "user@uia.no", EmailConfirmed = true, Nickname = "Arvin"};
        um.CreateAsync(user, "Password1.").Wait();


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