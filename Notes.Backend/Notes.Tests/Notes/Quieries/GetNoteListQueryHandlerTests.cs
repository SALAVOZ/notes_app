using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System.Threading;
using Xunit;

namespace Notes.Tests.Notes.Quieries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.dbContext;
            _mapper = fixture.mapper;
        }
        [Fact]
        public async void GetNoteListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetNoteListQueryHandler(_context, _mapper);

            // Act
            var result = await handler.Handle(new GetNoteListQuery
            {
                UserId = NotesContextFactory.UserBId
            }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(2);
        }
    }
}
