using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.DBContext;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Repository.NhanVT
{
    public class SampleThinhLcRepository : GenericRepository<SampleThinhLc>
    {
        public SampleThinhLcRepository() { }

        public SampleThinhLcRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<SampleThinhLc>> GetAllSamplesAsync()
        {
            var allSamples = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .ToListAsync();
            return allSamples ?? new List<SampleThinhLc>();
        }

        public async Task<SampleThinhLc> GetSampleByIdAsync(int id)
        {
            var sample = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .FirstOrDefaultAsync(s => s.SampleThinhLcid == id);
            return sample ?? new SampleThinhLc();
        }

        public async Task<List<SampleThinhLc>> GetSamplesByProfileIdAsync(int profileId)
        {
            var samples = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .Where(s => s.ProfileThinhLcid == profileId)
                .ToListAsync();
            return samples ?? new List<SampleThinhLc>();
        }
        
        public async Task<List<SampleThinhLc>> GetSamplesBySampleTypeIdAsync(int sampleTypeId)
        {
            var samples = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .Where(s => s.SampleTypeThinhLcid == sampleTypeId)
                .ToListAsync();
            return samples ?? new List<SampleThinhLc>();
        }

        public async Task<List<SampleThinhLc>> GetSamplesByAppointmentIdAsync(int appointmentId)
        {
            var samples = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .Where(s => s.AppointmentsTienDmid == appointmentId)
                .ToListAsync();
            return samples ?? new List<SampleThinhLc>();
        }

        public async Task<List<SampleThinhLc>> GetProcessedSamplesAsync()
        {
            var processedSamples = await _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .Where(s => s.IsProcessed == true)
                .ToListAsync();
            return processedSamples ?? new List<SampleThinhLc>();
        }

        public async Task<PaginationResult<List<SampleThinhLc>>> SearchSamplesWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize)
        {
            var samplesQuery = _context.SampleThinhLcs
                .Include(s => s.ProfileThinhLc)
                .Include(s => s.SampleTypeThinhLc)
                .Include(s => s.AppointmentsTienDm)
                .AsQueryable();

            if (profileId.HasValue)
            {
                samplesQuery = samplesQuery.Where(s => s.ProfileThinhLcid == profileId.Value);
            }

            if (sampleTypeId.HasValue)
            {
                samplesQuery = samplesQuery.Where(s => s.SampleTypeThinhLcid == sampleTypeId.Value);
            }

            if (isProcessed.HasValue)
            {
                samplesQuery = samplesQuery.Where(s => s.IsProcessed == isProcessed.Value);
            }

            var samples = await samplesQuery.ToListAsync();
            var totalItems = samples.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            samples = samples.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<SampleThinhLc>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = samples
            };
            return result;
        }

        public async Task<PaginationResult<List<SampleThinhLc>>> SearchWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize)
        {
            return await SearchSamplesWithPagingAsync(profileId, sampleTypeId, isProcessed, page, pageSize);
        }
    }
}
