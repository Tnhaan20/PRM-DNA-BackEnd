﻿using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.TienDM.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAppointmentsTienDm = DNATestingSystem.Repository.NhanVT.ModelExtensions.SearchAppointmentsTienDm;

namespace DNATestingSystem.Services.NhanVT
{
    public class AppointmentsTienDmService : IAppointmentsTienDmService
    {
        private readonly AppointmentsTienDmRepository _repository;
        public AppointmentsTienDmService()
            => _repository = new AppointmentsTienDmRepository();

        public async Task<int> CreateAsync(AppointmentsTienDm appointmentsTien)
        {
            return await _repository.CreateAsync(appointmentsTien);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<List<AppointmentsTienDm>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<PaginationResult<List<AppointmentsTienDm>>> GetAllPaginatedAsync(int page, int pageSize)
        {
            // Use the new optimized repository method
            return await _repository.GetAllPaginatedAsync(page, pageSize) ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<AppointmentsTienDm> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(int id, string contactPhone, decimal totalAmount, int page, int pageSize)
        {
            var paginationResult = await _repository.SearchAsync(id, contactPhone, totalAmount, page, pageSize);
            return paginationResult ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<PaginationResult<List<AppointmentsTienDm>>> SearchAsync(SearchAppointmentsTienDm searchRequest)
        {
            var paginationResult = await _repository.SearchAsync(searchRequest);
            return paginationResult ?? new PaginationResult<List<AppointmentsTienDm>>();
        }

        public async Task<int> UpdateAsync(AppointmentsTienDm appointmentsTien)
        {
            return await _repository.UpdateAsync(appointmentsTien);
        }

        // DTO Methods
        /// <summary>
        /// Create appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> CreateFromDtoAsync(CreateAppointmentsTienDmDto createDto)
        {
            return await _repository.CreateFromDtoAsync(createDto);
        }

        /// <summary>
        /// Update appointment using DTO to avoid navigation property validation issues
        /// </summary>
        public async Task<int> UpdateFromDtoAsync(UpdateAppointmentsTienDmDto updateDto)
        {
            return await _repository.UpdateFromDtoAsync(updateDto);
        }

        /// <summary>
        /// Get appointment with related data for display
        /// </summary>
        public async Task<AppointmentsTienDmDisplayDto?> GetDisplayDtoByIdAsync(int id)
        {
            return await _repository.GetDisplayDtoByIdAsync(id);
        }

        /// <summary>
        /// Get paginated appointments as display DTOs
        /// </summary>
        public async Task<PaginationResult<List<AppointmentsTienDmDisplayDto>>> GetDisplayDtosPaginatedAsync(SearchAppointmentsTienDm searchRequest)
        {
            return await _repository.GetDisplayDtosPaginatedAsync(searchRequest);
        }
    }
}
