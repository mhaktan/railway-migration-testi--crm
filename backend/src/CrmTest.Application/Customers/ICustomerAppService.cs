using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CrmTest.Analytics.Dto;
using CrmTest.Customers.Dto;

namespace CrmTest.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<
        CustomerDto,
        long,
        PagedCustomerResultRequestDto,
        CreateCustomerDto,
        CustomerDto>
    {
        Task<CustomerReportDto> GetReportData(long id);
    }
}
