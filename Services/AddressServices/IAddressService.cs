using contactManagerAPI.Models.AddressModels;

namespace contactManagerAPI.Services.AddressServices
{
    public interface IAddressService
    {
        Task<AddressModel> CreateAddress(AddressModel Req);
        Task<bool> DeactivateAddress(int AddressID);
        Task<AddressModel> UpdateAddress(AddressModel Req);
        Task<ICollection<AddressModel>> GetEntityAddresses(AddressModel Req);
        Task<AddressModel> GetEntityAddress(AddressModel Req);
    }
}
