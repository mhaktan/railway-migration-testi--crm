using System;
using Abp.Application.Services.Dto;

namespace CrmTest.Customers.Dto
{
    public class PagedCustomerResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
