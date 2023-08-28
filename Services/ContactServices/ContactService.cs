using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Services.ContactRepository;
using contactManagerAPI.Services.MiscRepository;
using Serilog;

namespace contactManagerAPI.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAuditService _auditor;
        private readonly IMiscRepository _numberRepository;
        private readonly IUserRepository _userRepository;

        public ContactService(
            IContactRepository contactRepository,
            IAuditService auditor,
            IMiscRepository numberRepository,
            IUserRepository userRepository
        )
        {
            _contactRepository = contactRepository;
            _auditor = auditor;
            _numberRepository = numberRepository;
            _userRepository = userRepository;
        }

        public async Task<SvcResponse<int>> CreateContact(ContactDTO req)
        {
            var ContactID = await _contactRepository.CreateContact(req);
            var res = new SvcResponse<int>();
            if (ContactID is not 0)
            {
                res.Data = ContactID;
                res.Message = "201";

                if (req.Numbers != null && req.Numbers.Any())
                {
                    foreach (var number in req.Numbers)
                    {
                        number.OwnerID = ContactID;
                        number.OwnerType = "Contact";
                        bool numAdded = await _numberRepository.AddContactNumber(number);
                        if (!numAdded)
                        {
                            Log.Error(
                                $"userRequest id={req.UserID} unable to add number={number} for Contact ID={ContactID}"
                            );
                        }
                    }

                    await _auditor.LogEvent(
                        req.UserID,
                        "Create Contact",
                        "User",
                        $"New Contact created for user ID={req.UserID}, ContactID={ContactID}"
                    );
                }
            }
            else
            {
                res.Data = 0;
                res.Message = "400";
                res.Success = false;
                res.Error = "Bad Request - Duplicate Contact.";
            }
            return res;
        }

        public async Task<SvcResponse<string>> DeactivateContact(ContactDTO req)
        {
            var res = new SvcResponse<string>();
            Contact? contactData = await _contactRepository.GetContactByID(req.ID);
            if (contactData != null)
            {
                res.Data = "Contact has been successfully deactivated.";
                res.Message = "200";
                _ = _contactRepository.DeactivateContact(req.ID);
            }
            else
            {
                res.Success = false;
                res.Error = "Bad Request - Contact does not exist!";
                res.Message = "400";
                res.Data = "Wrong password was entered";
            }
            return res;
        }

        public async Task<SvcResponse<IEnumerable<ContactDTO>>> GetAllContacts(int UserID)
        {
            var contacts = await _contactRepository.GetAllContacts(UserID);
            foreach (var contact in contacts)
            {
                contact.Numbers = await _numberRepository.GetContactNumbers(OwnerID: contact.ID);
            }
            var res = new SvcResponse<IEnumerable<ContactDTO>>
            {
                Data = contacts,
                Error =
                    !contacts.Any() || contacts is null ? "No contacts available in database" : "",
                Message = "200"
            };
            return res;
        }

        public async Task<SvcResponse<string>> UpdateContact(ContactDTO req)
        {
            var res = new SvcResponse<string>();
            var taskSuccess = await _contactRepository.UpdateContact(req);
            if (taskSuccess)
            {
                if (req.Numbers != null && req.Numbers.Any())
                {
                    // TODO
                    // CHECK NUMBERS IF A NEW NUMBER IS INCLUDED
                    // IF ANY NUMBER DOES NOT EXIST IN THE DATABASE
                    // WHICH MEANS IT HAS A NO ID WHEN IT SENT
                    // CREATE A NEW ENTRY IN DATABASE
                    foreach (var number in req.Numbers)
                    {
                        bool numUpdated = await _numberRepository.UpdateContactNumber(number);
                        if (!numUpdated)
                            Log.Error("$unable to update number");
                    }
                }
                res.Data = "Contact updated successfully!";
                res.Message = "201";
            }
            else
            {
                res.Success = false;
                res.Message = "400";
                res.Error = "Bad Request - Something went wrong";
            }
            return res;
        }
    }
}
