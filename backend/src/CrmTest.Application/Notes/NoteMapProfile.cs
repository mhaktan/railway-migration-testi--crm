using AutoMapper;
using CrmTest.Entities;
using CrmTest.Notes.Dto;

namespace CrmTest.Notes
{
    public class NoteMapProfile : Profile
    {
        public NoteMapProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<CreateNoteDto, Note>();
            CreateMap<NoteDto, Note>();
        }
    }
}
