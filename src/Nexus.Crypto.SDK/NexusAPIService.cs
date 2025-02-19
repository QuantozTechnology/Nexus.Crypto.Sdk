using System.Diagnostics.CodeAnalysis;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.PriceChartModel.cs;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK;

public class NexusAPIService(INexusApiClientFactory nexusApiClientFactory) : INexusAPIService, INexusBrokerAPIService
{
    private readonly Dictionary<string, string> _headers = [];
    private string? _baseAddress = null;

    private static async Task HandleErrorResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsAsync<CustomResultHolder<T>>();

        var exception = content.Errors != null && content.Errors.Length > 0 ?
            new NexusApiException($"Request failed: {content.Errors.Aggregate((a, b) => a + ", " + b)}") :
            new NexusApiException($"Request failed: {response.ReasonPhrase} ({(int)response.StatusCode})");

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

        if (_baseAddress != null)
        {
            client.BaseAddress = new Uri(_baseAddress);
        }

        return client;
    }

    public NexusAPIService SetBaseAddress([StringSyntax(StringSyntaxAttribute.Uri)] string baseAddress)
    {
        _baseAddress = baseAddress;
        return this;
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

        return await httpResponse.Content.ReadAsAsync<T>();
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
            Values = new GetBrokerBalances_1_1()
            {
                Balances = result.Values.ToList()
            }
        };
    }

    public async Task<CustomResultHolder<GetCustodianBalances_1_2>> GetCustodianBalances()
    {
        return await GetAsync<CustomResultHolder<GetCustodianBalances_1_2>>("balances", "1.2");
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

    public async Task<CustomResultHolder<GetTransaction>> GetTransaction(string txCode)
    {
        return await GetAsync<CustomResultHolder<GetTransaction>>($"transaction/{txCode}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetTransaction>>> GetTransactions(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransaction>>>(
            $"transaction{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetTransactionTotals(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<TotalsResult<TransactionTotals>>>(
            $"transaction/totals{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransfer>>>(
            $"/transfers{CreateUriQuery(queryParams)}", "1.2");
    }

    public async Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode, string cryptoCode)
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

    public async Task<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>> GetCallbacks(string transactionCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>>(
            $"/transaction/{transactionCode}/callbacks",
            "1.2");
    }
}
