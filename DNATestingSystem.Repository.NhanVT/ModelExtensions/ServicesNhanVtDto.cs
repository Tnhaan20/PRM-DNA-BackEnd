using System;

namespace DNATestingSystem.Repository.NhanVT.ModelExtensions
{
    /// <summary>
    /// Data Transfer Object for ServicesNhanVt without navigation properties
    /// </summary>
    public class ServicesNhanVtDto
    {
        public int ServicesNhanVtid { get; set; }

        public int ServiceCategoryNhanVtid { get; set; }

        public string ServiceName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? Duration { get; set; }

        public bool? IsSelfSampleAllowed { get; set; }

        public bool? IsHomeVisitAllowed { get; set; }

        public bool? IsClinicVisitAllowed { get; set; }

        public int? ProcessingTime { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsActive { get; set; }
        
        // Include the category name for convenience without including the full navigation property
        public string CategoryName { get; set; }
    }
}
