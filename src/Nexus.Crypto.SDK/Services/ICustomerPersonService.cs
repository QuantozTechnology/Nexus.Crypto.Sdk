using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public interface ICustomerPersonService
{
    Task<CustomResultHolder<PersonResponse>> CreateCustomerPerson(string customerCode, CreateCustomerPersonRequest request);
    Task<CustomResultHolder<PagedResult<PersonResponse>>> GetCustomerPersons(string customerCode, Dictionary<string, string> queryParams);
    Task<CustomResultHolder<PersonResponse>> UpdateCustomerPerson(string customerCode, Guid personId, UpdateCustomerPersonRequest request);
    Task DeleteCustomerPerson(string customerCode, Guid personId);
}