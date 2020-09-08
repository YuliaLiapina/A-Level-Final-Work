using System;
using System.Data.Entity;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IIdentityServerContext : IDisposable
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<UserPost> UserPosts { get; set; }
    }
}
