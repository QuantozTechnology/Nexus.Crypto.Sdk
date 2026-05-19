using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class CustomerService(BaseService service) : ICustomerService
{
    public Task<CustomResultHolder<GetCustomerTraceSummary[]>> GetCustomerTraceSummary(string customerCode,
        DateTime startDate)
    {
        return service.GetAsync<CustomResultHolder<GetCustomerTraceSummary[]>>(
            $"customer/{customerCode}/trace/summary?startDate={startDate.ToString(BaseService.ISO8601DateTimeFormat)}",
            BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<PagedResult<GetCustomerTrace>>> GetCustomerTraces(string customerCode,
        DateTime startDate)
    {
        return service.GetAsync<CustomResultHolder<PagedResult<GetCustomerTrace>>>(
            $"customer/{customerCode}/trace?startDate={startDate.ToString(BaseService.ISO8601DateTimeFormat)}",
            BaseService.ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode)
    {
        return await service.GetAsync<CustomResultHolder<GetCustomer>>($"customer/{customerCode}",
            BaseService.ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams)
    {
        return await service.GetAsync<CustomResultHolder<PagedResult<GetCustomer>>>(
            $"customer{BaseService.CreateUriQuery(queryParams)}",
            BaseService.ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request)
    {
        return await service.PostAsync<CreateCustomerRequest, CustomResultHolder<CreateCustomerResponse>>(
            "customer",
            request,
            BaseService.ApiVersion1_2
        );
    }

    public async Task<CustomResultHolder<DeleteCustomerResponse>> DeleteCustomer(string customerCode)
    {
        return await service.DeleteAsync<CustomResultHolder<DeleteCustomerResponse>>($"customer/{customerCode}",
            BaseService.ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetCustomer>> UpdateCustomer(string customerCode,
        UpdateCustomerRequest request)
    {
        return await service.PutAsync<UpdateCustomerRequest, CustomResultHolder<GetCustomer>>(
            $"customer/{customerCode}",
            request,
            BaseService.ApiVersion1_2);
    }
}