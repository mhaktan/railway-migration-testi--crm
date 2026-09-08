using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CrmTest.Customers.Dto
{
    [AutoMapTo(typeof(Entities.Customer))]
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

    }
}