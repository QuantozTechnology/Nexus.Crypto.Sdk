using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.Custodian;
using Nexus.Crypto.SDK.Models.PriceChartModel;
using Nexus.Crypto.SDK.Models.Response;
using Nexus.Crypto.SDK.Services;

namespace Nexus.Crypto.SDK;

public class NexusAPIService(INexusApiClientFactory nexusApiClientFactory)
    : BaseService(nexusApiClientFactory), INexusBrokerAPIService, INexusCustodianAPIService
{
    public IDocumentStoreSettingsService DocumentStoreSettings { get; } = new DocumentStoreSettingsService(nexusApiClientFactory);
    public IDocumentStoreTypeService DocumentStoreType { get; } = new DocumentStoreTypeService(nexusApiClientFactory);
    public IDocumentStoreRecordService  DocumentStoreRecord { get; } = new DocumentStoreRecordService(nexusApiClientFactory);
    public ICustomerService CustomerService { get; } = new CustomerService(nexusApiClientFactory);

    public NexusAPIService AddHeader(string key, string value)
    {
        _headers.Add(key, value);
        return this;
    }

    public async Task<CustomResultHolder<GetCurrencies>> GetCurrencies()
    {
        return await GetAsync<CustomResultHolder<GetCurrencies>>("currencies", "1.2");
    }

    public async Task<CustomResultHolder<GetPrices>> GetPrices(string currency)
    {
        return await GetAsync<CustomResultHolder<GetPrices>>($"prices/{currency}", "1.2");
    }

    public async Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner()
    {
        return await GetAsync<CustomResultHolder<GetLabelPartner>>("labelpartner", "1.2");
    }

    public async Task<CustomResultHolder<GetReserves>> GetReserves(string? reservesTimeStamp = null)
    {
        return await GetAsync<CustomResultHolder<GetReserves>>(
            $"reserves?reservesTimeStamp={reservesTimeStamp}",
            "1.2");
    }

    public async Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances()
    {
        var result = await GetAsync<Dictionary<string, BalanceItem_1_1>>("labelpartner/balance", "1.1");

        return new CustomResultHolder<GetBrokerBalances_1_1>
        {
            Values = new GetBrokerBalances_1_1 { Balances = result.Values.ToList() }
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
            $"balances/hotwallet/mutations{CreateUriQuery(queryParams)}",
            "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetMail>>>(
            $"mail{CreateUriQuery(queryParams)}",
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

    public async Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(GetTransfersRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransfer>>>(
            $"/transfers?{QueryParameterHelper.ToQueryString(request)}", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<ListOrder>>> GetOrders(GetOrdersRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<ListOrder>>>(
            $"/orders?{QueryParameterHelper.ToQueryString(request)}", "1.2");
    }

    public async Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode,
        string cryptoCode)
    {
        return await GetAsync<IEnumerable<ChartSeriesModelPT>>(
            $"api/MinuteChart/GetDefault/{timeSpan}?currency={currencyCode}&dcCode={cryptoCode}",
            "1.0");
    }

    public async Task<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>> GetCallbacks(
        string transactionCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>>(
            $"transaction/{transactionCode}/callbacks",
            "1.2");
    }

    public Task<CustomResultHolder<ListCustodianTransactionResponse>> GetCustodianTransaction(string txCode)
    {
        return GetAsync<CustomResultHolder<ListCustodianTransactionResponse>>(
            $"transactions/custodian/{txCode}",
            "1.2");
    }

    public Task<CustomResultHolder<CustodianCancelResponse>> CancelCustodianTransaction(string txCode)
    {
        return PostAsync<CustomResultHolder<CustodianCancelResponse>>(
            $"transactions/custodian/{txCode}/cancel",
            "1.2");
    }

    public async Task<CustomResultHolder<List<GetPortfolio>>> GetPortfolios()
    {
        return await GetAsync<CustomResultHolder<List<GetPortfolio>>>("portfolios", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<GetTrustLevel>>> GetTrustLevels()
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTrustLevel>>>("labelpartner/trustlevels", "1.2");
    }

    public async Task<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>> GetCustomerBankAccounts(string customerCode, Dictionary<string, string> queryParams)
    {
        return  await GetAsync<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>>(
            $"customer/{customerCode}/bankaccounts{CreateUriQuery(queryParams)}",
            "1.2");
    }

    public async Task<CustomResultHolder<CustomerBankAccountResponse>> CreateCustomerBankAccount(string customerCode, CreateBankAccountRequestModel request)
    {
        return await PostAsync<CreateBankAccountRequestModel, CustomResultHolder<CustomerBankAccountResponse>>(
            $"customer/{customerCode}/bankaccounts", request, "1.2");
    }

    public async Task<CustomResultHolder<CustomerBankAccountResponse>> UpdateCustomerBankAccount(string customerCode,
        Guid bankAccountId, UpdateBankAccountRequest request)
    {
        return await PutAsync<UpdateBankAccountRequest, CustomResultHolder<CustomerBankAccountResponse>>(
            $"customer/{customerCode}/bankaccounts/{bankAccountId}",
            request,
            "1.2"
        );
    }

    public Task DeleteCustomerBankAccount(string customerCode, Guid bankAccountId)
    {
        return DeleteAsync(
            $"customer/{customerCode}/bankAccounts/{bankAccountId}",
            "1.2");
    }
}