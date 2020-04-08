using RssReader.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DbChannel = RssReader.DataAccess.Model.Channel;
using RssReader.DataAccess;
using RssReader.Services.Models.Interfaces;

namespace RssReader.Services.Repository
{
    public class ChannelRepository : IRepository<Channel>
    {
        private ApplicationDbContext context;
        private Mapper mapper;

        public ChannelRepository()
        {
            context = new ApplicationDbContext();
        }

        public void AddItem(BaseModel entity)
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Channel, DbChannel>()));

            var dbChannel = mapper.Map<Channel, DbChannel>((Channel) entity);

            context.Channels.Add(dbChannel);

            context.SaveChanges();
        }

        public IEnumerable<Channel> GetAll()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<DbChannel, Channel>()));

            var dbChannels = context.Channels.ToList();

            return dbChannels.Select(c => mapper.Map<DbChannel, Channel>(c)).ToList();
        }

        public async Task<IEnumerable<Channel>> GetAllAsync()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<DbChannel, Channel>()));

            var dbChannels = await context.Channels.ToListAsync();

            return dbChannels.Select(c => mapper.Map<DbChannel, Channel>(c)).ToList();
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
