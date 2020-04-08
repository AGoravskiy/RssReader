using RssReader.ConsoleApp.Interfaces;
using RssReader.Services.Models;
using RssReader.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssReader.ConsoleApp.Services
{
    public class ConsoleAppServices
    {
        private IRepository<Channel> ChannelRepository;
        private IRepository<Post> PostRepository;
        private IMessageService MessageService;
        private IEnumerable<string> RssFeedsUrls;
        private int AddedPosts { get; set; }

        public ConsoleAppServices(IRepository<Channel> channelRepository, 
            IRepository<Post> postRepository, 
            IMessageService messageService, 
            IEnumerable<string> rssFeedsUrls)
        {
            ChannelRepository = channelRepository;
            PostRepository = postRepository;
            MessageService = messageService;
            RssFeedsUrls = rssFeedsUrls;
        }

        public void StartApp()
        {
            foreach (var rssFeedUrl in RssFeedsUrls)
            {
                var xmlDocument = LoadXml(rssFeedUrl);

                ReadChannel(xmlDocument);

                ReadPosts(xmlDocument);
            }
        }

        private XmlDocument LoadXml(string url)
        {
            var rssXmlDoc = new XmlDocument();

            rssXmlDoc.Load(url);

            return rssXmlDoc;
        }

        private void ReadChannel(XmlDocument rssXmlDoc)
        {
            var xmlNodeList = rssXmlDoc.SelectNodes("rss/channel");

            var channel = ReadChannelData(xmlNodeList[0]);

            AddChannel(channel);
        }

        private Channel ReadChannelData(XmlNode xmlNode)
        {
            var channel = new Channel();

            channel.Title = ReadNodeData(xmlNode, "title");

            channel.Link = ReadNodeData(xmlNode, "link");

            channel.Description = ReadNodeData(xmlNode, "description");

            channel.RssSourceLink = channel.Link.Split('/')[2];

            MessageService.CurrentChannel(channel.RssSourceLink);

            return channel;
        }

        private void AddChannel(Channel channel)
        {
            var channels = ChannelRepository.GetAll();

            if (channels.All(c => c.Link != channel.Link))
            {
                ChannelRepository.AddItem(channel);
            }
        }

        private void ReadPosts(XmlDocument rssXmlDoc)
        {
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            MessageService.ReadedPostsNumber(rssNodes.Count);

            AddedPosts = 0;

            foreach (XmlNode rssNode in rssNodes)
            {
                var post = ReadPostData(rssNode);

                AddPost(post);
            }

            MessageService.AddedPostsNumber(AddedPosts);
        }

        private Post ReadPostData(XmlNode xmlNode)
        {
            var post = new Post();

            post.Title = ReadNodeData(xmlNode, "title");

            post.Link = ReadNodeData(xmlNode, "link");

            post.Description = ReadNodeData(xmlNode, "description");

            post.Guid = ReadNodeData(xmlNode, "guid");

            post.PubDate = Convert.ToDateTime(ReadNodeData(xmlNode, "pubDate"));

            return post;
        }

        private void AddPost(Post post)
        {
            var posts = PostRepository.GetAll();

            if (PostRepository.GetAll().All(p => p.Guid != post.Guid))
            {
                var channelId = ChannelRepository.GetAll().FirstOrDefault(c => c.RssSourceLink == post.Link.Split('/')[2]).Id;

                post.ChannelId = channelId;

                PostRepository.AddItem(post);

                AddedPosts++;
            }
        }

        private string ReadNodeData(XmlNode xmlNode, string xpath)
        {
            var rssSubNode = xmlNode.SelectSingleNode(xpath);

            return rssSubNode != null ? rssSubNode.InnerText : "";
        }
    }
}
