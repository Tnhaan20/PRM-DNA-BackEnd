using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{
    public class SampleThinhLcService : ISampleThinhLcService
    {
        private readonly SampleThinhLcRepository _repository;

        public SampleThinhLcService()
            => _repository = new SampleThinhLcRepository();

        public async Task<List<SampleThinhLc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SampleThinhLc> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<SampleThinhLc>> GetByProfileIdAsync(int profileId)
        {
            return await _repository.GetSamplesByProfileIdAsync(profileId);
        }

        public async Task<List<SampleThinhLc>> GetBySampleTypeIdAsync(int sampleTypeId)
        {
            return await _repository.GetSamplesBySampleTypeIdAsync(sampleTypeId);
        }

        public async Task<List<SampleThinhLc>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _repository.GetSamplesByAppointmentIdAsync(appointmentId);
        }

        public async Task<List<SampleThinhLc>> GetProcessedSamplesAsync()
        {
            return await _repository.GetProcessedSamplesAsync();
        }

        public async Task<int> CreateAsync(SampleThinhLc sample)
        {
            return await _repository.CreateAsync(sample);
        }

        public async Task<int> UpdateAsync(SampleThinhLc sample)
        {
            return await _repository.UpdateAsync(sample);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.RemoveAsync(entity);
        }

        public async Task<PaginationResult<List<SampleThinhLc>>> SearchWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize)
        {
            return await _repository.SearchWithPagingAsync(profileId, sampleTypeId, isProcessed, page, pageSize);
        }
        
        // Legacy method implementations for compatibility
        public async Task<List<SampleThinhLc>> GetAllSamplesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<SampleThinhLc> GetSampleByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<List<SampleThinhLc>> GetSamplesByProfileIdAsync(int profileId)
        {
            return await GetByProfileIdAsync(profileId);
        }

        public async Task<List<SampleThinhLc>> GetSamplesByAppointmentIdAsync(int appointmentId)
        {
            return await GetByAppointmentIdAsync(appointmentId);
        }

        public async Task<int> CreateSampleAsync(SampleThinhLc sample)
        {
            return await CreateAsync(sample);
        }

        public async Task<int> UpdateSampleAsync(SampleThinhLc sample)
        {
            return await UpdateAsync(sample);
        }

        public async Task<bool> DeleteSampleAsync(int id)
        {
            return await DeleteAsync(id);
        }

        public async Task<PaginationResult<List<SampleThinhLc>>> SearchSamplesWithPagingAsync(int? profileId, int? sampleTypeId, bool? isProcessed, int page, int pageSize)
        {
            return await SearchWithPagingAsync(profileId, sampleTypeId, isProcessed, page, pageSize);
        }
    }
}
