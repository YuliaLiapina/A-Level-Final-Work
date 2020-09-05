using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class ContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleAuthor = new IdentityRole { Name = "author" };
            var roleUser = new IdentityRole { Name = "user" };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleAuthor);
            roleManager.Create(roleUser);

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com", FullName = "Administrator" };
            string password = "admin@gmail.com1A";
            var result = userManager.Create(admin, password);

            if(result.Succeeded)
            {
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleAuthor.Name);
                userManager.AddToRole(admin.Id, roleUser.Name);
            }

            var comments = new List<Comment>
            {
                new Comment
                {
                    PublishDate = DateTime.Now,
                    Body = "My first comment",
                    IsBlocked = false,
                    Author = admin
                },
                new Comment
                {
                    PublishDate = DateTime.Now,
                    Body = "My second comment",
                    IsBlocked = false,
                    Author = admin
                }
            };
            var posts = new List<Post>();

            for (int i = 0; i < 25; i++)
            {
                posts.Add( new Post
                {
                    PublishDate = DateTime.Now,
                    Title = $"Post {i}: Is the car next to you driverless? NHTSA launches new tool to help you find out",
                    Discription = "Description {i}: The U.S. Department of Transportation's new tracking tool identifies autonomous vehicles and testing sites",
                    Image = "https://o.aolcdn.com/images/dims3/GLOB/legacy_thumbnail/800x450/format/jpg/quality/85/https://s.aolcdn.com/os/ab/_cms/2020/09/02121422/avtest-screen.jpg.jpg",
                    Body = "<p>The U.S. Department of Transportation announced Wednesday that it has launched a new tool to improve the transparency of driverless vehicle testing programs. The new map-based tool is part of the Automated Vehicle Transparency and Engagement for Safe Testing (AV TEST) Initiative, which is being overseen by the National Highway Traffic Safety Administration (NHTSA).</p>" +
                           "<p>\"This tool gives the public online access to data about the on-road testing of automated driving systems so the public can understand more about this new technology,\" said Transportation Secretary Elaine L. Chao in the agency's announcement.</p>" +
                           "<p>While the tool does not actually track autonomous vehicles on the road in real-time — at least not in its current implementation — it does provide background information regarding current driverless vehicle operations and the organizations conducting said programs. The screenshot below, for example, shows information regarding the autonomy project Toyota is currently running out of its R&D center in Ann Arbor, Mich.</p>",
                    IsBlocked = false,
                    IsShowComment = true,
                    Author = admin
                });
            }


            posts[0].Comments.Add(comments[0]);
            posts[0].Comments.Add(comments[1]);

            context.Comments.AddRange(comments);
            context.Posts.AddRange(posts);

            context.SaveChanges();
        }
    }
}
