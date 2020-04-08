using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.DataAccess.Model
{
    public class Channel : BaseRssModel
    {
        public string RssSourceLink { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
