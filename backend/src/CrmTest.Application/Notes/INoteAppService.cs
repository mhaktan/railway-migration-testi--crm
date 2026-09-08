using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CrmTest.Analytics.Dto;
using CrmTest.Notes.Dto;

namespace CrmTest.Notes
{
    public interface INoteAppService : IAsyncCrudAppService<
        NoteDto,
        long,
        PagedNoteResultRequestDto,
        CreateNoteDto,
        NoteDto>
    {
        List<GroupCountDto> GetGroupedCount(NoteGroupedCountInput input);
    }
}
