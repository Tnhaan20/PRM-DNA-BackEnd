using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DNATestingSystem.Repository.NhanVT.Models;

public partial class UserServiceNhanVt
{
    public int UserServiceNhanVtid { get; set; }

    public int UserAccountId { get; set; }

    public int ServicesNhanVtid { get; set; }

    public DateTime? AssignedDate { get; set; }

    public bool? IsActive { get; set; }

    public string? Role { get; set; }

    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    [JsonIgnore]
    public virtual SystemUserAccount? CreatedByNavigation { get; set; }

    [JsonIgnore]
    public virtual SystemUserAccount? ModifiedByNavigation { get; set; }

    [JsonIgnore]
    public virtual ServicesNhanVt ServicesNhanVt { get; set; } = null!;

    [JsonIgnore]
    public virtual SystemUserAccount UserAccount { get; set; } = null!;
}
