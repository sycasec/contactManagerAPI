using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.ContactModels;

namespace contactManagerAPI.Mappers
{
    /// <summary>
    /// Mapper class to define mappings between Contact entities and models.
    /// </summary>
    public class ContactMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactMapper"/> class.
        /// </summary>
        public ContactMapper()
        {
            CreateMap<Contact, GetContactModel>().ReverseMap();

            CreateMap<Contact, UpsertContactModel>().ReverseMap();
        }
    }
}
