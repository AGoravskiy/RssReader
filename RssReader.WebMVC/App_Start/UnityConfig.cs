
using RssReader.Services.Models;
using RssReader.Services.Models.Interfaces;
using RssReader.Services.Repository;
using RssReader.Services.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace RssReader.WebMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IRepository<Channel>, ChannelRepository>();
            container.RegisterType<IRepository<Post>, PostRepository>();
            container.RegisterType<IRssServices, RssServices>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}