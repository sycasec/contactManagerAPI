using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.ContactModels;
using contactManagerAPI.Repositories.ContactRepository;
using contactManagerAPI.Repositories.UserRepository;
using contactManagerAPI.Services.AuditServices;

namespace contactManagerAPI.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public ContactService(
            IContactRepository contactRepository,
            IAuditService auditor,
            IMapper mapper
        )
        {
            _contactRepository = contactRepository;
            _auditService = auditor;
            _mapper = mapper;
        }

        public async Task<GetContactModel> CreateContact(int UserID, UpsertContactModel Req)
        {
            var newContact = _mapper.Map<Contact>(Req);
            newContact.UserID = UserID;
            var Res = await _contactRepository.CreateContact(newContact);
            if (Res > 0)
            {
                newContact.ID = Res;

                await _auditService.LogEvent(
                    Res,
                    "ContactService.CreateContact()",
                    "Contact",
                    $"Contact {Res} successfully created by user {UserID}"
                );

                return _mapper.Map<GetContactModel>(newContact);
            }
            throw new Exception("Failed to add contact");
        }

        public async Task<bool> DeactivateContact(int ContactID)
        {
            var contactDeactivated = await _contactRepository.DeactivateContact(ContactID);
            if (!contactDeactivated)
            {
                throw new Exception("Failed to delete contact");
            }
            await _auditService.LogEvent(
                ContactID,
                "ContactService.DeactivateContact()",
                "Contact",
                $"Contact {ContactID} successfully deactivated"
            );

            return contactDeactivated;
        }

        public async Task<ICollection<GetContactModel>> GetUserContacts(int UserID)
        {
            var contacts = await _contactRepository.GetAllContacts(UserID);
            return _mapper.Map<ICollection<GetContactModel>>(contacts);
        }

        public async Task<GetContactModel> GetUserContact(int ContactID)
        {
            var contact = await _contactRepository.GetContactByID(ContactID);
            if (contact == null)
                throw new Exception("Contact not found");
            return _mapper.Map<GetContactModel>(contact);
        }

        public async Task<GetContactModel> UpdateContact(int ContactID, UpsertContactModel Req)
        {
            var contact = await _contactRepository.GetContactByID(ContactID);
            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            var updateRequest = _mapper.Map<Contact>(Req);
            var contactUpdated = await _contactRepository.UpdateContact(contact, updateRequest);
            if (!contactUpdated)
                throw new Exception("Failed to update contact");

            var res = _mapper.Map<GetContactModel>(updateRequest);
            res.ID = ContactID;

            await _auditService.LogEvent(
                ContactID,
                "ContactService.UpdateContact",
                "Contact",
                $"Contact {ContactID} successfully updated"
            );

            return res;
        }
    }
}
