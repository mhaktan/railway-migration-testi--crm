using System;
using System.Collections.Generic;
using CrmTest.Notes.Dto;

namespace CrmTest.Customers.Dto
{
    /// <summary>
    /// Rapor verisi — kok kayit ve alt koleksiyonlar tek yanitta.
    /// PDF sablonu tek apiBinding kullandigi icin nested donuyoruz.
    /// </summary>
    public class CustomerReportDto
    {
        public CustomerDto Data { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
