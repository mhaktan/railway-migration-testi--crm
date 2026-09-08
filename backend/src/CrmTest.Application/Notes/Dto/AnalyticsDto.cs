using System;
using CrmTest.Analytics.Dto;

namespace CrmTest.Notes.Dto
{
    /// <summary>GetAll ile ayni filtreleri kabul eder, ustune GroupBy alir.</summary>
    public class NoteGroupedCountInput : PagedNoteResultRequestDto
    {
        public string GroupBy { get; set; }
    }

    public class NoteStatsInput : PagedNoteResultRequestDto
    {
        /// <summary>avg | sum | min | max | avgDayDiff</summary>
        public string Aggregate { get; set; }
        public string Field { get; set; }
        public string FromField { get; set; }
        public string ToField { get; set; }
    }
}
