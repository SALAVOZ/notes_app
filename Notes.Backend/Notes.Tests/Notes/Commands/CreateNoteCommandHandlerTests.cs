
using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Domain;
using Notes.Tests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            // Arrang
            var handler = new CreateNoteCommandHandler(context);
            var noteTitle = "note Name";
            var noteDetails = "note Details";
            //Act
            var noteId = await handler.Handle(new CreateNoteCommand
            {
                Details = noteDetails,
                Title = noteTitle,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(await context.Notes.SingleOrDefaultAsync(note => note.Id == noteId && note.Title == noteTitle && note.Details == noteDetails));
        }
    }
}
