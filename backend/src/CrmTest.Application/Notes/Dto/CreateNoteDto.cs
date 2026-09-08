using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CrmTest.Notes.Dto
{
    [AutoMapTo(typeof(Entities.Note))]
    public class CreateNoteDto
    {
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public DateTime NoteDate { get; set; }

        public long CustomerId { get; set; }

    }
}