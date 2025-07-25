﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DNATestingSystem.Repository.NhanVT.Models;

public partial class ServicesNhanVt
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

    [JsonIgnore]
    public virtual ServiceCategoriesNhanVt ServiceCategoryNhanVt { get; set; }

    // Navigation property for appointments - JsonIgnore to prevent circular reference
    [JsonIgnore]
    public virtual ICollection<AppointmentsTienDm> AppointmentsTienDms { get; set; } = new List<AppointmentsTienDm>();

    // Navigation property for user services - JsonIgnore to prevent circular reference
    [JsonIgnore]
    public virtual ICollection<UserServiceNhanVt> UserServiceNhanVts { get; set; } = new List<UserServiceNhanVt>();
}