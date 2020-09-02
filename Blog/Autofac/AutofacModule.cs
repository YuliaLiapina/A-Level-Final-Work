using Autofac;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Models;
using DataAccessLayer.Models;

namespace Blog.Autofac
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostModel,Post>().ReverseMap();
                cfg.CreateMap<PostViewModel,PostModel>().ReverseMap();
                cfg.CreateMap<CommentModel,Comment>().ReverseMap();
                cfg.CreateMap<CommentViewModel,CommentModel>().ReverseMap();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
