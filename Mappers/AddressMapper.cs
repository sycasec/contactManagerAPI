using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.AddressModels;

namespace contactManagerAPI.Mappers
{
    /// <summary>
    /// Mapper class to define mappings between Contact entities and models.
    /// </summary>
    public class AddressMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressMapper"/> class.
        /// </summary>
        public AddressMapper()
        {
            CreateMap<Address, AddressModel>().ReverseMap();
        }
    }
}
