using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CrmTest.Entities
{
    [Table("Customers")]
    public class Customer : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

    }
}