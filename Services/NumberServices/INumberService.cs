using contactManagerAPI.Models.NumberModels;

namespace contactManagerAPI.Services.NumberServices
{
    public interface INumberService
    {
        Task<NumberModel> CreateNumber(NumberModel Req);
        Task<bool> DeactivateNumber(int NumberID);
        Task<NumberModel> UpdateNumber(NumberModel Req);
        Task<ICollection<NumberModel>> GetEntityNumbers(NumberModel Req);
        Task<NumberModel> GetEntityNumber(NumberModel Req);
    }
}
