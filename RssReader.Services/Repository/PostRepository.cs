using RssReader.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DbPost = RssReader.DataAccess.Model.Post;
using RssReader.DataAccess;
using RssReader.Services.Models.Interfaces;

namespace RssReader.Services.Repository
{
    public class PostRepository : IRepository<Post>
    {
        private ApplicationDbContext context;
        private Mapper mapper;

        public PostRepository()
        {
            context = new ApplicationDbContext();
        }

        public void AddItem(BaseModel entity)
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Post, DbPost>()));

            var dbPost = mapper.Map<Post, DbPost>((Post) entity);

            context.Posts.Add(dbPost);

            context.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<DbPost, Post>()));

            var dbPosts = context.Posts.ToList();
            
            return dbPosts.Select(p => mapper.Map<DbPost, Post>(p)).ToList();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<DbPost, Post>()));

            var dbPosts = await context.Posts.ToListAsync();

            return dbPosts.Select(p => mapper.Map<DbPost, Post>(p)).ToList();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
