﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Repository.NhanVT.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; } = 1;

        public int? PageSize { get; set; } = 10;
    }

    public class SearchServices : SearchRequest
    {
        public string? serviceName { get; set; }

        public decimal? servicePrice { get; set; }

        public string? categoryName { get; set; }

    }
}