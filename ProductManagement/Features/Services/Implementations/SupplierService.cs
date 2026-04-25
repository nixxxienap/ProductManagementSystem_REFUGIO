using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync() =>
            await _supplierRepository.GetAllAsync();

        public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync() =>
            await _supplierRepository.GetActiveSuppliersAsync();

        public async Task<Supplier?> GetSupplierByIdAsync(int id) =>
            await _supplierRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword) =>
            await _supplierRepository.SearchAsync(keyword);

        public async Task<(bool Success, string Message)> CreateSupplierAsync(Supplier supplier)
        {
            if (!string.IsNullOrEmpty(supplier.Email) && await _supplierRepository.ExistsAsync(supplier.Email))
                return (false, $"Supplier with email '{supplier.Email}' already exists.");

            await _supplierRepository.AddAsync(supplier);
            return (true, "Supplier created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateSupplierAsync(Supplier supplier)
        {
            var existing = await _supplierRepository.GetByIdAsync(supplier.Id);
            if (existing is null)
                return (false, "Supplier not found.");

            await _supplierRepository.UpdateAsync(supplier);
            return (true, "Supplier updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteSupplierAsync(int id)
        {
            var existing = await _supplierRepository.GetByIdAsync(id);
            if (existing is null)
                return (false, "Supplier not found.");

            await _supplierRepository.DeleteAsync(id);
            return (true, "Supplier deleted successfully.");
        }
    }
}
