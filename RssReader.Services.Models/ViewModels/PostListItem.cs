using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models.ViewModels
{
    public class PostListItem
    {
        [Display(Name = "Название новости")]
        public string Title { get; set; }

        [Display(Name = "Описание новости")]
        public string Description { get; set; }

        [Display(Name = "Источник")]
        public string ChannelTitle { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime? pubDate { get; set; }

        public string Link { get; set; }
    }
}
