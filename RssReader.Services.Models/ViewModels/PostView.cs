using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models.ViewModels
{
    public class PostView
    {
        private static SortType defaultSortType = SortType.None;
        public int? ChannelId { get; set; }
        public SortType SortProperty { get; set; } = defaultSortType;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
    }
}
