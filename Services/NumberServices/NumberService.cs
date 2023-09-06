using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.NumberModels;
using contactManagerAPI.Repositories.NumberRepository;
using contactManagerAPI.Services.AuditServices;

namespace contactManagerAPI.Services.NumberServices
{
    public class NumberService : INumberService
    {
        private readonly INumberRepository _numberRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public NumberService(
            INumberRepository numberRepository,
            IAuditService auditService,
            IMapper mapper
        )
        {
            _numberRepository = numberRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<NumberModel> CreateNumber(NumberModel Req)
        {
            var newNumber = _mapper.Map<ContactNumber>(Req);
            var numberAdded = await _numberRepository.AddContactNumber(newNumber);
            if (!numberAdded)
            {
                throw new Exception("Failed to add number");
            }

            await _auditService.LogEvent(
                Req.OwnerID,
                "NumberService.CreateNumber()",
                $"{Req.Type} -> Number",
                $"Number {Req.Number} successfully added for Entity {Req.OwnerID}"
            );

            return _mapper.Map<NumberModel>(newNumber);
        }

        public async Task<bool> DeactivateNumber(int NumberID)
        {
            var numberDeactivated = await _numberRepository.DeactivateContactNumber(NumberID);
            if (!numberDeactivated)
                throw new Exception("Failed to delete number");
            await _auditService.LogEvent(
                NumberID,
                "NumberService.DeactivateNumber()",
                "Number",
                $"Number {NumberID} successfully deactivated"
            );

            return numberDeactivated;
        }

        public async Task<NumberModel> GetEntityNumber(NumberModel Req)
        {
            var number = await _numberRepository.GetContactNumber(
                Req.ID,
                Req.OwnerID,
                Req.OwnerType
            );
            if (number == null)
                throw new Exception("Number not found");
            return _mapper.Map<NumberModel>(number);
        }

        public async Task<ICollection<NumberModel>> GetEntityNumbers(NumberModel Req)
        {
            var numbers = await _numberRepository.GetContactNumbers(Req.OwnerID, Req.OwnerType);
            return _mapper.Map<ICollection<NumberModel>>(numbers);
        }

        public async Task<NumberModel> UpdateNumber(NumberModel Req)
        {
            var existing = await _numberRepository.GetContactNumber(
                Req.ID,
                Req.OwnerID,
                Req.OwnerType
            );
            if (existing == null)
                throw new Exception("Number not found");

            var updateRequest = _mapper.Map<ContactNumber>(Req);
            var numberUpdated = await _numberRepository.UpdateContactNumber(
                existing,
                updateRequest
            );
            if (!numberUpdated)
                throw new Exception("Failed to update number");

            await _auditService.LogEvent(
                Req.ID,
                "NumberService.UpdateNumber()",
                "Number",
                $"Number {Req.Number} of owner {Req.OwnerID} successfully updated!"
            );

            return Req;
        }
    }
}
