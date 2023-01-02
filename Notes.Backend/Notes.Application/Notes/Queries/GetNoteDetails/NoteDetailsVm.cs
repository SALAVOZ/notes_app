using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class NoteDetailsVm : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditTime { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteDetailsVm>()
                .ForMember(notevm => notevm.Title,
                    opt => opt.MapFrom(note => note.Title))
                .ForMember(notevm => notevm.Details,
                    opt => opt.MapFrom(note => note.Details))
                .ForMember(notevm => notevm.Id,
                    opt => opt.MapFrom(note => note.Id))
                .ForMember(notevm => notevm.CreationDate,
                    opt => opt.MapFrom(note => note.CreationDate))
                .ForMember(notevm => notevm.EditTime,
                    opt => opt.MapFrom(note => note.EditDate));

        }
    }
}
