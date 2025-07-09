using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DNATestingSystem.Repository.NhanVT.Models;

public partial class LociPhienNt
{
    public int PhienNtid { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsCodis { get; set; }

    public string? Description { get; set; }

    public decimal? MutationRate { get; set; }

    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<AlleleResultsPhienNt> AlleleResultsPhienNts { get; set; } = new List<AlleleResultsPhienNt>();

    [JsonIgnore]
    public virtual ICollection<LocusMatchResultsPhienNt> LocusMatchResultsPhienNts { get; set; } = new List<LocusMatchResultsPhienNt>();
}
