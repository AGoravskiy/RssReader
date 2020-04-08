using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.DataAccess.Model
{
    public abstract class BaseRssModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
