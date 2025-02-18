using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.PriceChartModel.cs;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK;

public class NexusAPIService : INexusAPIService
{
    private readonly INexusApiClientFactory _nexusApiClientFactory;
    public NexusAPIService(INexusApiClientFactory nexusApiClientFactory)
    {
        _nexusApiClientFactory = nexusApiClientFactory;
    }

    private void HandleErrorResponse(HttpResponseMessage response)
    {
        var content = response.Content.ReadAsAsync<CustomResultHolder<object>>().Result;

        var exception = content.Errors != null && content.Errors.Length > 0 ?
            new NexusApiException($"Request failed: {content.Errors.Aggregate((a, b) => a + ", " + b)}") :
            new NexusApiException($"Request failed: {response.ReasonPhrase} ({(int)response.StatusCode})");

        exception.StatusCode = response.StatusCode;
        exception.ResponseContent = response.Content.ReadAsStringAsync().Result;

        throw exception;
    }

    public async Task<HttpClient> GetApiClient()
    {
        return await _nexusApiClientFactory.GetClient();
    }

    private async Task<HttpResponseMessage> GetAsync(string endPoint, string apiVersion)
    {
        var client = await _nexusApiClientFactory.GetClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            HandleErrorResponse(httpResponse);
        }

        return httpResponse;
    }

    public async Task<CustomResultHolder<GetCurrencies>> GetCurrencies()
    {
        var endPoint = $"currencies";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetCurrencies>>();
    }

    public async Task<CustomResultHolder<GetCustomer>> GetCustomer(string customerCode)
    {
        var endPoint = $"customer/{customerCode}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetCustomer>>();
    }

    public async Task<CustomResultHolder<PagedResult<GetCustomer>>> GetCustomers(Dictionary<string, string> queryParams)
    {
        var endPoint = $"customer{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<PagedResult<GetCustomer>>>();
    }

    public async Task<CustomResultHolder<GetPrices>> GetPrices(string currency)
    {
        var endPoint = $"prices/{currency}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetPrices>>();
    }

    public async Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner()
    {
        throw new NotImplementedException("GetLabelPartner");
        //var endPoint = $"labelpartner";

        //var result = await GetAsync(endPoint, "1.2");

        //return await result.Content.ReadAsAsync<CustomResultHolder<GetLabelPartner>>();
    }

    public async Task<CustomResultHolder<GetReserves>> GetReserves(string reservesTimeStamp = null)
    {
        var endPoint = $"reserves?reservesTimeStamp={reservesTimeStamp}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetReserves>>();
    }

    public async Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances()
    {
        var endPoint = $"labelpartner/balance";

        var result = await GetAsync(endPoint, "1.1");

        var content = await result.Content.ReadAsAsync<Dictionary<string, BalanceItem_1_1>>();
        var contentList = new List<BalanceItem_1_1>(content.Count);

        foreach (var c in content)
        {
            contentList.Add(c.Value);
        }

        return new CustomResultHolder<GetBrokerBalances_1_1>()
        {
            Values = new GetBrokerBalances_1_1()
            {
                Balances = contentList
            }
        };
    }

    public async Task<CustomResultHolder<GetCustodianBalances_1_2>> GetCustodianBalances()
    {
        var endPoint = $"balances";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetCustodianBalances_1_2>>();
    }

    public async Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(
        Dictionary<string, string> queryParams)
    {
        var endPoint = $"/balances/hotwallet/mutations{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<PagedResult<GetBalanceMutation>>>();
    }

    public async Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(
        Dictionary<string, string> queryParams)
    {
        var endPoint = $"/mail{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<PagedResult<GetMail>>>();
    }

    public async Task<CustomResultHolder<GetTransaction>> GetTransaction(string txCode)
    {
        var endPoint = $"transaction/{txCode}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<GetTransaction>>();
    }

    public async Task<CustomResultHolder<PagedResult<GetTransaction>>> GetTransactions(Dictionary<string, string> queryParams)
    {
        var endPoint = $"transaction{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<PagedResult<GetTransaction>>>();
    }

    public async Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetTransactionTotals(Dictionary<string, string> queryParams)
    {
        var endPoint = $"transaction/totals{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<TotalsResult<TransactionTotals>>>();
    }

    public async Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(Dictionary<string, string> queryParams)
    {
        var endPoint = $"/transfers{CreateUriQuery(queryParams)}";

        var result = await GetAsync(endPoint, "1.2");

        return await result.Content.ReadAsAsync<CustomResultHolder<PagedResult<GetTransfer>>>();
    }

    public async Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode, string cryptoCode)
    {
        var endPoint = $"api/MinuteChart/GetDefault/{timeSpan}?currency={currencyCode}&dcCode={cryptoCode}";

        var result = await GetAsync(endPoint, "1.0");

        return await result.Content.ReadAsAsync<IEnumerable<ChartSeriesModelPT>>();
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
}
