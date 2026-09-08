using System;
using Abp.Application.Services.Dto;

namespace CrmTest.Notes.Dto
{
    public class PagedNoteResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? CustomerId { get; set; }
        public string Text { get; set; }
        public DateTime? NoteDate { get; set; }
        public DateTime? NoteDateFrom { get; set; }
        public DateTime? NoteDateTo { get; set; }
    }
}
