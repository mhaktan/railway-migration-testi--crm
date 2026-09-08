using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CrmTest.Entities
{
    [Table("Notes")]
    public class Note : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public DateTime NoteDate { get; set; }

        public long CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

    }
}