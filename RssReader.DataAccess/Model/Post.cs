using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.DataAccess.Model
{
    public class Post : BaseRssModel
    {
        public string Guid { get; set; }
        public DateTime? PubDate { get; set; }
        public int ChannelId { get; set; }
    }
}
