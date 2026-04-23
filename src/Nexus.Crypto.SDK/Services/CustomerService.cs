using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class CustomerService(INexusApiClientFactory nexusApiClientFactory) :BaseService(nexusApiClientFactory), ICustomerService
{
    public Task<CustomResultHolder<GetCustomerTraceSummary[]>> GetCustomerTraceSummary(string customerCode,
        DateTime startDate)
    {
        return GetAsync<CustomResultHolder<GetCustomerTraceSummary[]>>(
            $"customer/{customerCode}/trace/summary?startDate={startDate.ToString(ISO8601DateTimeFormat)}",
            ApiVersion);
    }

    public Task<CustomResultHolder<PagedResult<GetCustomerTrace>>> GetCustomerTraces(string customerCode,
        DateTime startDate)
    {
        return GetAsync<CustomResultHolder<PagedResult<GetCustomerTrace>>>(
            $"customer/{customerCode}/trace?startDate={startDate.ToString(ISO8601DateTimeFormat)}",
            ApiVersion);
    }
    
    public async Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode)
    {
        return await GetAsync<CustomResultHolder<GetCustomer>>($"customer/{customerCode}", ApiVersion);
    }

    public async Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCustomer>>>(
            $"customer{CreateUriQuery(queryParams)}",
            ApiVersion);
    }

    public async Task<CustomResultHolder<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request)
    {
        return await PostAsync<CreateCustomerRequest, CustomResultHolder<CreateCustomerResponse>>(
            "customer",
            request,
            ApiVersion
        );
    }

    public async Task<CustomResultHolder<DeleteCustomerResponse>> DeleteCustomer(string customerCode)
    {
        return await DeleteAsync<CustomResultHolder<DeleteCustomerResponse>>($"customer/{customerCode}", ApiVersion);
    }
    
}