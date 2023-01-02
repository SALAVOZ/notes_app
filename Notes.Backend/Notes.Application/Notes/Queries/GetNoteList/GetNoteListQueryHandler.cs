using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    internal class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly IMapper _mapper;
        private readonly INotesDbContext _dbContext;
        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dbContext.Notes.Where(note => note.UserId == request.UserId).ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return new NoteListVm { Notes = notesQuery };
        }
    }
}
