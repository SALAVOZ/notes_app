
using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Quieries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext context;
        private readonly IMapper mapper;
        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            context = fixture.dbContext;
            mapper = fixture.mapper;
        }

        public async Task GetNoteDetailsQueryHandler()
        {
            // Arrange
            var handler = new GetNoteDetailsQueryHandler(context, mapper);

            // Act
            var result = await handler.Handle(new GetNoteDetailsQuery
            {
                Id = Guid.Parse("B6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                UserId = NotesContextFactory.UserBId 
            }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NoteDetailsVm>();
            result.Title.ShouldBe("Title2");
            result.CreationDate.ShouldBe(DateTime.Today);
        }
    }
}
