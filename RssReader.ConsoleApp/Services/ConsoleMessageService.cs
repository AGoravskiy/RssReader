using RssReader.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ConsoleApp.Services
{
    public class ConsoleMessageService : IMessageService
    {
        public void AddedPostsNumber(int addedPostsNumber)
        {
            Console.WriteLine("Added posts: " + addedPostsNumber);
        }

        public void CurrentChannel(string channelLink)
        {
            Console.WriteLine("Channel: " + channelLink);
        }

        public void ReadedPostsNumber(int readedPostsNumber)
        {
            Console.WriteLine("Read posts: " + readedPostsNumber);
        }
    }
}
