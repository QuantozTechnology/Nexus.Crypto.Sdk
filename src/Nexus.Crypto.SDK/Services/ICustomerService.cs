using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public interface ICustomerService
{
    Task<CustomResultHolder<GetCustomerTraceSummary[]>> GetCustomerTraceSummary(string customerCode,
        DateTime startDate);

    Task<CustomResultHolder<PagedResult<GetCustomerTrace>>> GetCustomerTraces(string customerCode,
        DateTime startDate);

    Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode);

    Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams);
    
    Task<CustomResultHolder<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request);
    
    Task<CustomResultHolder<DeleteCustomerResponse>> DeleteCustomer(string customerCode);
    
}