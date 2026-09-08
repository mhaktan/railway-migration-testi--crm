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
using CrmTest.Notes.Dto;
using CrmTest.Analytics.Dto;
using CrmTest.Authorization;

namespace CrmTest.Notes
{
    public class NoteAppService : AsyncCrudAppService<
        Note,
        NoteDto,
        long,
        PagedNoteResultRequestDto,
        CreateNoteDto,
        NoteDto>,
        INoteAppService
    {
        public NoteAppService(IRepository<Note, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Note_Read;
            GetAllPermissionName = PermissionNames.Note_Read;
            CreatePermissionName = PermissionNames.Note_Create;
            UpdatePermissionName = PermissionNames.Note_Update;
            DeletePermissionName = PermissionNames.Note_Delete;
        }

        protected override IQueryable<Note> CreateFilteredQuery(PagedNoteResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Text != null && x.Text.Contains(input.Keyword)))
                .WhereIf(!input.Text.IsNullOrWhiteSpace(), x => x.Text != null && x.Text.Contains(input.Text))
                .WhereIf(input.NoteDate.HasValue, x => x.NoteDate == input.NoteDate.Value)
                .WhereIf(input.NoteDateFrom.HasValue, x => x.NoteDate >= input.NoteDateFrom.Value)
                .WhereIf(input.NoteDateTo.HasValue, x => x.NoteDate <= input.NoteDateTo.Value)
                .WhereIf(input.CustomerId.HasValue, x => x.CustomerId == input.CustomerId.Value);
        }
        [Abp.Authorization.AbpAuthorize(PermissionNames.Note_Read)]
        public List<GroupCountDto> GetGroupedCount(NoteGroupedCountInput input)
        {
            // Whitelist — istemciden gelen alan adı doğrudan sorguya girmez.
            var allowed = new[] { "CustomerId" };
            if (input.GroupBy == null || !allowed.Contains(input.GroupBy))
            {
                throw new Abp.UI.UserFriendlyException(
                    $"Gruplanabilir alan degil: {input.GroupBy}. Izin verilenler: {string.Join(", ", allowed)}");
            }

            var query = CreateFilteredQuery(input);

            switch (input.GroupBy)
            {
                case "CustomerId":
                    return query
                        .GroupBy(x => new { Key = x.CustomerId, Label = x.Customer == null ? null : x.Customer.Name })
                        .Select(g => new GroupCountDto
                        {
                            Key = g.Key.Key == null ? null : g.Key.Key.ToString(),
                            Label = g.Key.Label ?? "(bos)",
                            Count = g.Count(),
                        })
                        .ToList();
                default:
                    return new List<GroupCountDto>();
            }
        }

    }
}
