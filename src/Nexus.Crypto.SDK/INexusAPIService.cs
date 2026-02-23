using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.Custodian;
using Nexus.Crypto.SDK.Models.PriceChartModel;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK;

public interface INexusAPIService
{
    Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner();
    Task<CustomResultHolder<GetCurrencies>> GetCurrencies();
    Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode);
    Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<GetPrices>> GetPrices(string currency);
    Task<CustomResultHolder<GetReserves>> GetReserves(string reservesTimeStamp);
    Task<CustomResultHolder<GetCustodianBalances>> GetCustodianBalances();
    Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances();
    Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(Dictionary<string, string> queryParams);

    Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(GetTransfersRequest? request = null);
    Task<CustomResultHolder<PagedResult<ListOrder>>> GetOrders(GetOrdersRequest? request = null);
    Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode, string cryptoCode);

    Task<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>> GetCallbacks(string transactionCode);

    Task<CustomResultHolder<GetCustomerTraceSummary[]>> GetCustomerTraceSummary(string customerCode, DateTime startDate);
    Task<CustomResultHolder<PagedResult<GetCustomerTrace>>> GetCustomerTraces(string customerCode, DateTime startDate);

    Task<CustomResultHolder<List<GetPortfolio>>> GetPortfolios();

    Task<CustomResultHolder<PagedResult<GetTrustLevel>>> GetTrustLevels();
    Task<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>> GetCustomerBankAccounts(string customerCode, Dictionary<string, string> queryParams);
    Task<CustomResultHolder<CustomerBankAccountResponse>> CreateCustomerBankAccount(string customerCode, CreateBankAccountRequestModel request);
    Task DeleteCustomerBankAccount(string customerCode, Guid bankAccountId);
    Task<CustomResultHolder<CustomerBankAccountResponse>> UpdateCustomerBankAccount(string customerCode, Guid bankAccountId, UpdateBankAccountRequest request);
}

public interface INexusBrokerAPIService : INexusAPIService
{
    Task<CustomResultHolder<GetTransaction>> GetBrokerTransaction(string txCode);
    Task<CustomResultHolder<PagedResult<GetTransaction>>> GetBrokerTransactions(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetBrokerTransactionTotals(Dictionary<string, string> queryParams);
}

public interface INexusCustodianAPIService : INexusAPIService
{
    Task<CustomResultHolder<ListCustodianTransactionResponse>> GetCustodianTransaction(string txCode);
    Task<CustomResultHolder<CustodianCancelResponse>> CancelCustodianTransaction(string txCode);
}