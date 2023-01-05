using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Tests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Notes.Domain;
using Notes.Application.Common.Exceptions;
using System;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.Tests.Notes.Commands
{
    public  class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteNoteCommandHandler(context);

            //Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(context.Notes.SingleOrDefault(note => note.Id == NotesContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None
                    ));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteNoteCommandHandler(context);
            var createHandler = new CreateNoteCommandHandler(context);
            var noteId = await createHandler.Handle(
                new CreateNoteCommand
                {
                    Title = "Title",
                    Details = "Details",
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = noteId,
                        UserId = NotesContextFactory.UserBId
                    }, CancellationToken.None
                    ));
        }
    }
}
