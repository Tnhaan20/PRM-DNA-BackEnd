using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DNATestingSystem.Repository.NhanVT.Models;

public partial class ProfileThinhLc
{
    public int ProfileThinhLcid { get; set; }

    public int? UserAccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? NationalId { get; set; }

    public bool IsDeceased { get; set; }

    public string? Notes { get; set; }

    public int? Count { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<AlleleResultsPhienNt> AlleleResultsPhienNts { get; set; } = new List<AlleleResultsPhienNt>();

    [JsonIgnore]
    public virtual ICollection<ProfileRelationshipThinhLc> ProfileRelationshipThinhLcProfileThinhLcid1Navigations { get; set; } = new List<ProfileRelationshipThinhLc>();

    [JsonIgnore]
    public virtual ICollection<ProfileRelationshipThinhLc> ProfileRelationshipThinhLcProfileThinhLcid2Navigations { get; set; } = new List<ProfileRelationshipThinhLc>();

    [JsonIgnore]
    public virtual ICollection<SampleThinhLc> SampleThinhLcs { get; set; } = new List<SampleThinhLc>();

    [JsonIgnore]
    public virtual SystemUserAccount? UserAccount { get; set; }
}
