using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.interfaces;
using Notes.Persistence;
using System;
using Xunit;

namespace Notes.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public NotesDbContext dbContext;
        public IMapper mapper;
        public QueryTestFixture()
        {
            dbContext = NotesContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()

        {
            NotesContextFactory.Destroy(dbContext); 
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
