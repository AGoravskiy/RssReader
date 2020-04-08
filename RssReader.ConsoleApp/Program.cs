using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RssReader.Services.Repository;
using RssReader.Services.Models;
using AutoMapper;
using RssReader.ConsoleApp.Services;
using RssReader.ConsoleApp.Interfaces;
using RssReader.Services.Models.Interfaces;

namespace RssReader.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageService messageService = new ConsoleMessageService();
            IEnumerable<string> rssFeedsUrls = new List<string> { "https://interfax.by/news/feed/", "https://habr.com/ru/rss/all/all/" };

            using (IRepository<Channel> channelRepository = new ChannelRepository())
            {
                using (IRepository<Post> postRepository = new PostRepository())
                {
                    var consoleAppServices = new ConsoleAppServices(channelRepository, postRepository, messageService, rssFeedsUrls);

                    consoleAppServices.StartApp();
                }
            }
        }
    }
}
