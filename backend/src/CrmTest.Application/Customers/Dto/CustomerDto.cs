using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CrmTest.Customers.Dto
{
    [AutoMapFrom(typeof(Entities.Customer))]
    public class CustomerDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}