using System;

namespace DNATestingSystem.Repository.NhanVT.ModelExtensions
{
    /// <summary>
    /// Data Transfer Object for ServiceCategoriesNhanVt without navigation properties
    /// </summary>
    public class ServiceCategoryNhanVtDto
    {
        public int ServiceCategoryNhanVtid { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
