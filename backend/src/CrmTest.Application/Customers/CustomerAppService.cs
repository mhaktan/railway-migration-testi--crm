using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CrmTest.Entities;
using CrmTest.Customers.Dto;
using CrmTest.Analytics.Dto;
using CrmTest.Notes.Dto;
using CrmTest.Authorization;

namespace CrmTest.Customers
{
    public class CustomerAppService : AsyncCrudAppService<
        Customer,
        CustomerDto,
        long,
        PagedCustomerResultRequestDto,
        CreateCustomerDto,
        CustomerDto>,
        ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Customer_Read;
            GetAllPermissionName = PermissionNames.Customer_Read;
            CreatePermissionName = PermissionNames.Customer_Create;
            UpdatePermissionName = PermissionNames.Customer_Update;
            DeletePermissionName = PermissionNames.Customer_Delete;
        }

        protected override IQueryable<Customer> CreateFilteredQuery(PagedCustomerResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Name != null && x.Name.Contains(input.Keyword)) ||
                    (x.Email != null && x.Email.Contains(input.Keyword)) ||
                    (x.Phone != null && x.Phone.Contains(input.Keyword)) ||
                    (x.City != null && x.City.Contains(input.Keyword)))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name != null && x.Name.Contains(input.Name))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email != null && x.Email.Contains(input.Email))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), x => x.Phone != null && x.Phone.Contains(input.Phone))
                .WhereIf(!input.City.IsNullOrWhiteSpace(), x => x.City != null && x.City.Contains(input.City))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value);
        }
        /// <summary>
        /// Rapor verisi — kok kayit ve alt koleksiyonlar TEK yanitta.
        /// PDF sablonu template basina tek apiBinding kullaniyor.
        /// </summary>
        [Abp.Authorization.AbpAuthorize(PermissionNames.Customer_Read)]
        public async Task<CustomerReportDto> GetReportData(long id)
        {
            var root = await Repository.GetAll()
                .Include(x => x.Notes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (root == null)
                throw new Abp.UI.UserFriendlyException($"Kayit bulunamadi: {id}");

            return new CustomerReportDto
            {
                Data = ObjectMapper.Map<CustomerDto>(root),
                Notes = ObjectMapper.Map<List<NoteDto>>(
                    root.Notes == null ? new List<Note>() : root.Notes.ToList()),
            };
        }

    }
}
