using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CrmTest.Notes.Dto
{
    [AutoMapFrom(typeof(Entities.Note))]
    public class NoteDto : EntityDto<long>
    {
        public string Text { get; set; }

        public DateTime NoteDate { get; set; }

        public long CustomerId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}