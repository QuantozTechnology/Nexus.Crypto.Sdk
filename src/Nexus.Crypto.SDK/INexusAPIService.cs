using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.PriceChartModel.cs;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK;

public interface INexusAPIService
{
    Task<HttpClient> GetApiClient();
    Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner();
    Task<CustomResultHolder<GetCurrencies>> GetCurrencies();
    Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode);
    Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<GetPrices>> GetPrices(string currency);
    Task<CustomResultHolder<GetReserves>> GetReserves(string reservesTimeStamp);
    Task<CustomResultHolder<GetCustodianBalances_1_2>> GetCustodianBalances();
    Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances();
    Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(Dictionary<string, string> queryParams);

    Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(Dictionary<string, string> queryParams);
    Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode, string cryptoCode);
}

public interface INexusBrokerAPIService : INexusAPIService
{
    Task<CustomResultHolder<GetTransaction>> GetTransaction(string txCode);
    Task<CustomResultHolder<PagedResult<GetTransaction>>> GetTransactions(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetTransactionTotals(Dictionary<string, string> queryParams);
}