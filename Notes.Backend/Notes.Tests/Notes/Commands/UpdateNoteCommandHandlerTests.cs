using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(context);
            var updatedTitle = "new title";

            // Act
            await handler.Handle(new UpdateNoteCommand
            {
                Title = updatedTitle,
                UserId = NotesContextFactory.UserBId,
                Id = NotesContextFactory.NoteIdForUpdate
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(context.Notes.SingleOrDefault(note => note.Id == NotesContextFactory.NoteIdForUpdate && note.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () => await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId,

                    }, CancellationToken.None
                    )
                );
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(context);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () => await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = NotesContextFactory.NoteIdForUpdate,
                        UserId = NotesContextFactory.UserAId

                    }, CancellationToken.None
                    )
                );
        }
    }
}
