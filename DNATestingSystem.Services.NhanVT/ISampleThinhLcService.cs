using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ISampleThinhLcService
    {
        Task<List<SampleThinhLc>> GetAllAsync();
        Task<SampleThinhLc> GetByIdAsync(int id);
        Task<List<SampleThinhLc>> GetByProfileIdAsync(int profileId);
        Task<List<SampleThinhLc>> GetBySampleTypeIdAsync(int sampleTypeId);
        Task<List<SampleThinhLc>> GetByAppointmentIdAsync(int appointmentId);
        Task<List<SampleThinhLc>> GetProcessedSamplesAsync();
        Task<int> CreateAsync(SampleThinhLc sample);
        Task<int> UpdateAsync(SampleThinhLc sample);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<SampleThinhLc>>> SearchWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize);
        
        // Legacy method names for compatibility
        Task<List<SampleThinhLc>> GetAllSamplesAsync();
        Task<SampleThinhLc> GetSampleByIdAsync(int id);
        Task<List<SampleThinhLc>> GetSamplesByProfileIdAsync(int profileId);
        Task<List<SampleThinhLc>> GetSamplesByAppointmentIdAsync(int appointmentId);
        Task<int> CreateSampleAsync(SampleThinhLc sample);
        Task<int> UpdateSampleAsync(SampleThinhLc sample);
        Task<bool> DeleteSampleAsync(int id);
        Task<PaginationResult<List<SampleThinhLc>>> SearchSamplesWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize);
    }
}
