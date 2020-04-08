using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models.ViewModels
{
    public class PagingPostsList
    {
        public IEnumerable<PostListItem> PostListItems { get; set; }
        public PostView PostView { get; set; }
    }
}
