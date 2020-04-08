using AutoMapper;
using RssReader.Services.Models;
using RssReader.Services.Models.Interfaces;
using RssReader.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Services
{
    public class RssServices : IRssServices
    {
        private readonly IRepository<Channel> _channelRepository;
        private readonly IRepository<Post> _postRepository;
        private static int PageSize = 10;

        public RssServices(IRepository<Channel> channelRepository, IRepository<Post> postRepository)
        {
            _channelRepository = channelRepository;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Channel>> GetChannelsAsync()
        {
            return await _channelRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<PagingPostsList> GetPagingPostsListAsync(PostView postView)
        {
            var dbPosts = await GetPostsAsync();
            var dbChannels = await GetChannelsAsync();

            return postView.ChannelId == null ? 
                GetAllChannelPagingPostsList(dbPosts, dbChannels, postView) : 
                GetOneChannelPagingPosts(dbPosts, dbChannels, postView);
        }

        public PagingPostsList GetAllChannelPagingPostsList(IEnumerable<Post> dbPosts, IEnumerable<Channel> dbChannels, PostView postView)
        {
            return FormPagingPostList(dbPosts, dbChannels, postView);
        }

        public PagingPostsList GetOneChannelPagingPosts(IEnumerable<Post> dbPosts, IEnumerable<Channel> dbChannels, PostView postView)
        {
            var dbPostsOneChannel = GetOneChannelPosts(dbPosts, postView.ChannelId);

            return FormPagingPostList(dbPostsOneChannel, dbChannels, postView);
        }

        private PagingPostsList FormPagingPostList(IEnumerable<Post> dbPosts, IEnumerable<Channel> dbChannels, PostView postView)
        {
            var postListItems = new List<PostListItem>();

            foreach (var dbPost in dbPosts)
            {
                postListItems.Add(ConvertToPostListItem(dbChannels, dbPost));
            }

            postListItems = SortPosts(postView.SortProperty, postListItems).ToList();

            postView.TotalPages = CalcTotalPages(postListItems);

            return new PagingPostsList { 
                PostListItems = SkipPages(postListItems, postView), 
                PostView = postView };
        }

        private int CalcTotalPages(List<PostListItem> postListItems)
        {
            return (int)Math.Ceiling((decimal)postListItems.Count / PageSize);
        }

        private List<PostListItem> SkipPages(List<PostListItem> postListItems, PostView postView)
        {
            return postListItems.Skip((postView.PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

        private IEnumerable<Post> GetOneChannelPosts(IEnumerable<Post> dbPosts, int? channelId)
        {
            return dbPosts.Where(p => p.ChannelId == channelId).ToList();
        }

        private PostListItem ConvertToPostListItem(IEnumerable<Channel> channels, Post post)
        {
            return new PostListItem()
            {
                ChannelTitle = channels.FirstOrDefault(c => c.Id == post.ChannelId).RssSourceLink,
                Description = post.Description,
                pubDate = post.PubDate,
                Title = post.Title,
                Link = post.Link
            };
        }

        private IEnumerable<PostListItem> SortPosts(SortType sortType, IEnumerable<PostListItem> postViews)
        {
            switch (sortType)
            {
                case SortType.ByDate:
                    postViews = postViews.OrderByDescending(p => p.pubDate);
                    break;
                case SortType.ByChannel:
                    postViews = postViews.OrderBy(p => p.ChannelTitle);
                    break;
                default:
                    break;
            }

            return postViews;
        }
    }
}
