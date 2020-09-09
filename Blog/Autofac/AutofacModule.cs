using Autofac;
using AutoMapper;
using Blog.Models;
using BusinessLayer.Models;
using DataAccessLayer;
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

                cfg.CreateMap<UserPostModel,UserPost>().ReverseMap();

                cfg.CreateMap<PostViewModel,EditPostViewModel>().ReverseMap();
                cfg.CreateMap<PostModel,EditPostViewModel>().ReverseMap();

                cfg.CreateMap<Awards,AwardsModel>().ReverseMap();
                cfg.CreateMap<AwardsModel,AwardsViewModel>().ReverseMap();
                cfg.CreateMap<AwardsModel,AwardEditViewModel>().ReverseMap();
                cfg.CreateMap<AwardsModel,CreateAwardViewModel>().ReverseMap();

                cfg.CreateMap<ApplicationUser,ApplicationUserModel>().ReverseMap();
                cfg.CreateMap<ApplicationUserModel,ApplicationUserViewModel>().ReverseMap();

                cfg.CreateMap<PostViewModel,CreatePostViewModel>().ReverseMap();
                cfg.CreateMap<PostModel,CreatePostViewModel>().ReverseMap();

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
