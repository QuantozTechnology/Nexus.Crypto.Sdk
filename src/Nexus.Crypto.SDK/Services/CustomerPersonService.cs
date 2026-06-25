using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class CustomerPersonService(BaseService service) : ICustomerPersonService
{
    public Task<CustomResultHolder<PersonResponse>> CreateCustomerPerson(string customerCode, CreateCustomerPersonRequest request)
    {
        return service.PostAsync<CreateCustomerPersonRequest, CustomResultHolder<PersonResponse>>(
            $"customer/{customerCode}/person",
            request,
            BaseService.ApiVersion1_2
        );
    }

    public Task<CustomResultHolder<PagedResult<PersonResponse>>> GetCustomerPersons(string customerCode, Dictionary<string, string> queryParams)
    {
        var url = $"customer/{customerCode}/person" + BaseService.ToQueryString(queryParams);
        return service.GetAsync<CustomResultHolder<PagedResult<PersonResponse>>>(
            url,
            BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<PersonResponse>> UpdateCustomerPerson(string customerCode, Guid personId, UpdateCustomerPersonRequest request)
    {
        return service.PutAsync<UpdateCustomerPersonRequest, CustomResultHolder<PersonResponse>>(
            $"customer/{customerCode}/person/{personId}",
            request,
            BaseService.ApiVersion1_2);
    }

    public Task DeleteCustomerPerson(string customerCode, Guid personId)
    {
        return service.DeleteAsync($"customer/{customerCode}/person/{personId}",  BaseService.ApiVersion1_2);
    }
}