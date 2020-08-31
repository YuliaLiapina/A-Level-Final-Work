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

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
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
                    IsBlocked = false
                },
                new Comment
                {
                    PublishDate = DateTime.Now,
                    Body = "My second comment",
                    IsBlocked = false
                }
            };

            var post = new Post
            {
                PublishDate = DateTime.Now,
                Title = "My first post",
                Body = "My first post",
                IsBlocked = false,
                IsShowComment = true
            };

            post.Comments.Add(comments[0]);
            post.Comments.Add(comments[1]);

            context.Comments.AddRange(comments);
            context.Posts.Add(post);

            context.SaveChanges();
        }
    }
}
