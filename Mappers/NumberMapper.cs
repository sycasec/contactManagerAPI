using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.NumberModels;

namespace contactManagerAPI.Mappers
{
    /// <summary>
    /// Mapper class to define mappings between Contact entities and models.
    /// </summary>
    public class NumberMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberMapper"/> class.
        /// </summary>
        public NumberMapper()
        {
            CreateMap<ContactNumber, NumberModel>().ReverseMap();
        }
    }
}
