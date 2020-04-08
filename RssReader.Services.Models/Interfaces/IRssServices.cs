using RssReader.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models.Interfaces
{
    public interface IRssServices
    {
        Task<IEnumerable<Channel>> GetChannelsAsync();
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<PagingPostsList> GetPagingPostsListAsync(PostView postView);
    }
}
