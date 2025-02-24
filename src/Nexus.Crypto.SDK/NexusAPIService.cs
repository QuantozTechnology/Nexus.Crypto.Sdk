using System.Net.Http.Json;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.Custodian;
using Nexus.Crypto.SDK.Models.PriceChartModel;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK;

public class NexusAPIService(INexusApiClientFactory nexusApiClientFactory)
    : INexusAPIService, INexusBrokerAPIService, INexusCustodianAPIService
{
    public const string ISO8601DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

    private readonly Dictionary<string, string> _headers = [];

    private static async Task HandleErrorResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<CustomResultHolder<T>>();

        var exception = content?.Errors is { Length: > 0 }
            ? new NexusApiException($"Request failed: {content.Errors.Aggregate((a, b) => a + ", " + b)}")
            : new NexusApiException($"Request failed: {response.ReasonPhrase} ({(int)response.StatusCode})");

        exception.StatusCode = response.StatusCode;
        exception.ResponseContent = await response.Content.ReadAsStringAsync();

        throw exception;
    }

    private async Task<HttpClient> GetApiClient(string apiVersion)
    {
        var client = await nexusApiClientFactory.GetClient(apiVersion);

        foreach (var header in _headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        return client;
    }

    public NexusAPIService AddHeader(string key, string value)
    {
        _headers.Add(key, value);
        return this;
    }

    private async Task<T> GetAsync<T>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<T>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<T>())!;
    }

    private async Task<T2> PostAsync<T1, T2>(string endPoint, T1? postObject, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PostAsJsonAsync(endPoint, postObject);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<T2>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<T2>())!;
    }

    private async Task<T2> PostAsync<T2>(string endPoint, string apiVersion)
    {
        return await PostAsync<object, T2>(endPoint, null, apiVersion);
    }

    public async Task<CustomResultHolder<GetCurrencies>> GetCurrencies()
    {
        return await GetAsync<CustomResultHolder<GetCurrencies>>("currencies", "1.2");
    }

    public async Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode)
    {
        return await GetAsync<CustomResultHolder<GetCustomer>>($"customer/{customerCode}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCustomer>>>(
            $"customer{CreateUriQuery(queryParams)}",
            "1.2");
    }

    public async Task<CustomResultHolder<GetPrices>> GetPrices(string currency)
    {
        return await GetAsync<CustomResultHolder<GetPrices>>($"prices/{currency}", "1.2");
    }

    public async Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner()
    {
        return await GetAsync<CustomResultHolder<GetLabelPartner>>("labelpartner", "1.2");
    }

    public async Task<CustomResultHolder<GetReserves>> GetReserves(string reservesTimeStamp = null)
    {
        return await GetAsync<CustomResultHolder<GetReserves>>(
            $"reserves?reservesTimeStamp={reservesTimeStamp}",
            "1.2");
    }

    public async Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances()
    {
        var result = await GetAsync<Dictionary<string, BalanceItem_1_1>>("labelpartner/balance", "1.1");

        return new CustomResultHolder<GetBrokerBalances_1_1>()
        {
            Values = new GetBrokerBalances_1_1() { Balances = result.Values.ToList() }
        };
    }

    public async Task<CustomResultHolder<GetCustodianBalances>> GetCustodianBalances()
    {
        return await GetAsync<CustomResultHolder<GetCustodianBalances>>("balances", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetBalanceMutation>>>(
            $"/balances/hotwallet/mutations{CreateUriQuery(queryParams)}",
            "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetMail>>>(
            $"/mail{CreateUriQuery(queryParams)}",
            "1.2");
    }

    public async Task<CustomResultHolder<GetTransaction>> GetBrokerTransaction(string txCode)
    {
        return await GetAsync<CustomResultHolder<GetTransaction>>($"transaction/{txCode}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetTransaction>>> GetBrokerTransactions(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransaction>>>(
            $"transaction{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetBrokerTransactionTotals(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<TotalsResult<TransactionTotals>>>(
            $"transaction/totals{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransfer>>>(
            $"/transfers{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode,
        string cryptoCode)
    {
        return await GetAsync<IEnumerable<ChartSeriesModelPT>>(
            $"api/MinuteChart/GetDefault/{timeSpan}?currency={currencyCode}&dcCode={cryptoCode}",
            "1.0");
    }

    /// <summary>
    /// Take Dictionary of query parameters and creates the query string to paste to the URI.
    /// Prepends the '?'. When the dictionary is empty, returns an empty string;
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private static string CreateUriQuery(Dictionary<string, string> queryParams)
    {
        var query = string.Empty;

        foreach (var p in queryParams)
        {
            if (query == string.Empty)
            {
                query += "?";
            }
            else
            {
                query += "&";
            }

            query += $"{p.Key}={p.Value}";
        }

        return query;
    }

    public async Task<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>> GetCallbacks(
        string transactionCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>>(
            $"transaction/{transactionCode}/callbacks",
            "1.2");
    }

    public Task<CustomResultHolder<GetCustomerTraceSummary[]>> GetCustomerTraceSummary(string customerCode,
        DateTime startDate)
    {
        return GetAsync<CustomResultHolder<GetCustomerTraceSummary[]>>(
            $"customer/{customerCode}/trace/summary?startDate={startDate.ToString(ISO8601DateTimeFormat)}",
            "1.2");
    }

    public Task<CustomResultHolder<PagedResult<GetCustomerTrace>>> GetCustomerTraces(string customerCode,
        DateTime startDate)
    {
        return GetAsync<CustomResultHolder<PagedResult<GetCustomerTrace>>>(
            $"customer/{customerCode}/trace?startDate={startDate.ToString(ISO8601DateTimeFormat)}",
            "1.2");
    }

    public Task<CustomResultHolder<ListCustodianTransactionResponse>> GetCustodianTransaction(string txCode)
    {
        return GetAsync<CustomResultHolder<ListCustodianTransactionResponse>>(
            $"/transactions/custodian/{txCode}",
            "1.2");
    }

    public Task<CustomResultHolder<CustodianCancelResponse>> CancelCustodianTransaction(string txCode)
    {
        return PostAsync<CustomResultHolder<CustodianCancelResponse>>(
            $"/transactions/custodian/{txCode}/cancel",
            "1.2");
    }
}