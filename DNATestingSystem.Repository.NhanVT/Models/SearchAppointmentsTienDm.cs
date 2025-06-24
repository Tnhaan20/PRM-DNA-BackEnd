using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Repository.NhanVT.Models
{
    public class SearchAppointmentsTienDm
    {
        public int? AppointmentsTienDmid { get; set; }
        public string? ContactPhone { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? UserAccountId { get; set; }
        public int? ServicesNhanVtid { get; set; }
        public int? AppointmentStatusesTienDmid { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public string? SamplingMethod { get; set; }
        public bool? IsPaid { get; set; }
    }
}
