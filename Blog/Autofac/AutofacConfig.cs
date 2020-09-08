using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLayer;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace Blog.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            builder.RegisterType<PostManager>().As<IPostManager>();
            builder.RegisterType<CommentManager>().As<ICommentManager>();
            builder.RegisterType<UserPostManager>().As<IUserPostManager>();

            builder.RegisterType<PostRepo>().As<IRepository<Post, int>>();
            builder.RegisterType<CommentRepo>().As<IRepository<Comment, int>>();
            builder.RegisterType<UserPostRepo>().As<IRepository<UserPost, int>>();

            builder.RegisterModule<AutofacModule>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
