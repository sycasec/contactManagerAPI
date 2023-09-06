using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.AddressModels;
using contactManagerAPI.Repositories.AddressRepository;
using contactManagerAPI.Services.AuditServices;

namespace contactManagerAPI.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _AddressRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public AddressService(
            IAddressRepository AddressRepository,
            IAuditService auditService,
            IMapper mapper
        )
        {
            _AddressRepository = AddressRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<AddressModel> CreateAddress(AddressModel Req)
        {
            var newAddress = _mapper.Map<Address>(Req);
            var AddressAdded = await _AddressRepository.CreateAddress(newAddress);
            if (!AddressAdded)
            {
                throw new Exception("Failed to add Address");
            }

            await _auditService.LogEvent(
                Req.OwnerID,
                "AddressService.CreateAddress()",
                $"{Req.Type} -> Address",
                $"Address {Req.ID} successfully added for Entity {Req.OwnerID}"
            );

            return _mapper.Map<AddressModel>(newAddress);
        }

        public async Task<bool> DeactivateAddress(int AddressID)
        {
            var AddressDeactivated = await _AddressRepository.DeactivateAddress(AddressID);
            if (!AddressDeactivated)
                throw new Exception("Failed to delete Address");
            await _auditService.LogEvent(
                AddressID,
                "AddressService.DeactivateAddress()",
                "Address",
                $"Address {AddressID} successfully deactivated"
            );

            return AddressDeactivated;
        }

        public async Task<AddressModel> GetEntityAddress(AddressModel Req)
        {
            var Address = await _AddressRepository.GetEntityAddress(
                AddressID: Req.ID,
                OwnerID: Req.OwnerID,
                OwnerType: Req.OwnerType
            );
            if (Address == null)
                throw new Exception("Address not found");
            return _mapper.Map<AddressModel>(Address);
        }

        public async Task<ICollection<AddressModel>> GetEntityAddresses(AddressModel Req)
        {
            var Addresss = await _AddressRepository.GetEntityAddresses(
                OwnerID: Req.OwnerID,
                OwnerType: Req.OwnerType
            );
            return _mapper.Map<ICollection<AddressModel>>(Addresss);
        }

        public async Task<AddressModel> UpdateAddress(AddressModel Req)
        {
            var existing = await _AddressRepository.GetEntityAddress(
                AddressID: Req.ID,
                OwnerID: Req.OwnerID,
                OwnerType: Req.OwnerType
            );
            if (existing == null)
                throw new Exception("Address not found");

            var updateRequest = _mapper.Map<Address>(Req);
            var AddressUpdated = await _AddressRepository.UpdateAddress(existing, updateRequest);
            if (!AddressUpdated)
                throw new Exception("Failed to update Address");

            await _auditService.LogEvent(
                Req.ID,
                "AddressService.UpdateAddress()",
                "Address",
                $"Address {Req.ID} of owner {Req.OwnerID} successfully updated!"
            );

            return Req;
        }
    }
}
