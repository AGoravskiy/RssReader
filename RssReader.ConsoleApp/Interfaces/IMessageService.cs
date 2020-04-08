using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ConsoleApp.Interfaces
{
    public interface IMessageService
    {
        void CurrentChannel(string channelLink);
        void ReadedPostsNumber(int readedPostsNumber);
        void AddedPostsNumber(int addedPostsNumber);
    }
}
