using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.AddressRepository
{
    public interface IAddressRepository
    {
        Task<bool> AddressExists(Address address);
        Task<bool> CreateAddress(Address request);
        Task<bool> UpdateAddress(Address existing, Address request);
        Task<bool> DeactivateAddress(int AddressID);
        Task<ICollection<Address>> GetEntityAddresses(int OwnerID, string OwnerType);
        Task<Address?> GetEntityAddress(int OwnerID, string OwnerType, int AddressID);
    }
}
