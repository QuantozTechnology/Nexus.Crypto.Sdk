using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Account;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.Custodian;
using Nexus.Crypto.SDK.Models.PriceChartModel;
using Nexus.Crypto.SDK.Models.Response;
using Nexus.Crypto.SDK.Services;

namespace Nexus.Crypto.SDK;

public class NexusAPIService(INexusApiClientFactory nexusApiClientFactory)
    : BaseService(nexusApiClientFactory),
        INexusBrokerAPIService,
        INexusCustodianAPIService
{
    public new NexusAPIService AddHeader(string key, string value)
    {
        base.AddHeader(key, value);
        return this;
    }

    public IDocumentStoreService DocumentStore => new DocumentStoreService(this);
    public ICustomerService Customer => new CustomerService(this);
    public ICustomerPersonService CustomerPerson => new CustomerPersonService(this);
    
    public async Task<CustomResultHolder<GetCurrencies>> GetCurrencies()
    {
        return await GetAsync<CustomResultHolder<GetCurrencies>>("currencies", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetPrices>> GetPrices(string currency)
    {
        return await GetAsync<CustomResultHolder<GetPrices>>($"prices/{currency}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner()
    {
        return await GetAsync<CustomResultHolder<GetLabelPartner>>("labelpartner", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetReserves>> GetReserves(string? reservesTimeStamp = null)
    {
        return await GetAsync<CustomResultHolder<GetReserves>>(
            $"reserves?reservesTimeStamp={reservesTimeStamp}",
            ApiVersion1_2);
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
        return await GetAsync<CustomResultHolder<GetCustodianBalances>>("balances", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetBalanceMutation>>>(
            $"balances/hotwallet/mutations{CreateUriQuery(queryParams)}",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetMail>>>(
            $"mail{CreateUriQuery(queryParams)}",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetTransaction>> GetBrokerTransaction(string txCode)
    {
        return await GetAsync<CustomResultHolder<GetTransaction>>($"transaction/{txCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetTransaction>>> GetBrokerTransactions(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransaction>>>(
            $"transaction{CreateUriQuery(queryParams)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetBrokerTransactionTotals(
        Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<TotalsResult<TransactionTotals>>>(
            $"transaction/totals{CreateUriQuery(queryParams)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(GetTransfersRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTransfer>>>(
            $"/transfers?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<ListOrder>>> GetOrders(GetOrdersRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<ListOrder>>>(
            $"/orders?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
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
            ApiVersion1_2);
    }

    public Task<CustomResultHolder<ListCustodianTransactionResponse>> GetCustodianTransaction(string txCode)
    {
        return GetAsync<CustomResultHolder<ListCustodianTransactionResponse>>(
            $"transactions/custodian/{txCode}",
            ApiVersion1_2);
    }

    public Task<CustomResultHolder<CustodianCancelResponse>> CancelCustodianTransaction(string txCode)
    {
        return PostAsync<CustomResultHolder<CustodianCancelResponse>>(
            $"transactions/custodian/{txCode}/cancel",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<List<GetPortfolio>>> GetPortfolios()
    {
        return await GetAsync<CustomResultHolder<List<GetPortfolio>>>("portfolios", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetTrustLevel>>> GetTrustLevels()
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetTrustLevel>>>("labelpartner/trustlevels", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>> GetCustomerBankAccounts(
        string customerCode, Dictionary<string, string> queryParams)
    {
        return await GetAsync<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>>(
            $"customer/{customerCode}/bankaccounts{CreateUriQuery(queryParams)}",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustomerBankAccountResponse>> CreateCustomerBankAccount(string customerCode,
        CreateBankAccountRequestModel request)
    {
        return await PostAsync<CreateBankAccountRequestModel, CustomResultHolder<CustomerBankAccountResponse>>(
            $"customer/{customerCode}/bankaccounts", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustomerBankAccountResponse>> UpdateCustomerBankAccount(string customerCode,
        Guid bankAccountId, UpdateBankAccountRequest request)
    {
        return await PutAsync<UpdateBankAccountRequest, CustomResultHolder<CustomerBankAccountResponse>>(
            $"customer/{customerCode}/bankaccounts/{bankAccountId}",
            request,
            ApiVersion1_2
        );
    }

    public Task DeleteCustomerBankAccount(string customerCode, Guid bankAccountId)
    {
        return DeleteAsync(
            $"customer/{customerCode}/bankAccounts/{bankAccountId}",
            ApiVersion1_2);
    }

    // ── Label partner ──────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<LabelPartnerResponse>> GetLabelPartnerConfig()
    {
        return await GetAsync<CustomResultHolder<LabelPartnerResponse>>("labelpartner/config", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<LabelPartnerCryptoCurrency>>> GetLabelPartnerCryptoCurrencies(
        GetLabelPartnerCryptoCurrenciesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<LabelPartnerCryptoCurrency>>>(
            $"labelpartner/cryptocurrencies?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<LabelPartnerCountry>>> GetLabelPartnerCountries(
        GetLabelPartnerCountriesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<LabelPartnerCountry>>>(
            $"labelpartner/countries?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    // ── Prices ─────────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<GetPriceData>> GetHistoricPrices(string currency, string crypto,
        GetPriceDataRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<GetPriceData>>(
            $"prices/{currency}/{crypto}/historic?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    // ── Balances ───────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<HotWalletBalance>>> GetHotWalletBalances(
        GetHotWalletBalancesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<HotWalletBalance>>>(
            $"balances/hotwallet?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    // ── Accounts ───────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetAccountResponse>>> GetAccounts(
        GetAccountsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetAccountResponse>>>(
            $"accounts?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetAccountResponse>> GetAccount(string accountCode)
    {
        return await GetAsync<CustomResultHolder<GetAccountResponse>>($"accounts/{accountCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CreateAccountResponse>> CreateAccount(string customerCode,
        CreateAccountRequest request)
    {
        return await PostAsync<CreateAccountRequest, CustomResultHolder<CreateAccountResponse>>(
            $"customer/{customerCode}/accounts", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetAccountResponse>> UpdateAccount(string accountCode,
        UpdateAccountRequest request)
    {
        return await PutAsync<UpdateAccountRequest, CustomResultHolder<GetAccountResponse>>(
            $"accounts/{accountCode}", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CreateAccountResponse>> CreateNonNativeAccount(string accountCode,
        CreateNonNativeAccountRequest request)
    {
        return await PostAsync<CreateNonNativeAccountRequest, CustomResultHolder<CreateAccountResponse>>(
            $"accounts/{accountCode}/nonnative", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetAccountBalanceResponse>>> GetAccountBalances(
        string accountCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetAccountBalanceResponse>>>(
            $"accounts/{accountCode}/balances", ApiVersion1_2);
    }

    // ── Buckets ────────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetBucketResponse>>> GetBuckets(
        GetBucketsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetBucketResponse>>>(
            $"buckets?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetBucketResponse>> GetBucket(string bucketCode)
    {
        return await GetAsync<CustomResultHolder<GetBucketResponse>>($"buckets/{bucketCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetBucketResponse>> CreateBucket(CreateBucketRequest request)
    {
        return await PostAsync<CreateBucketRequest, CustomResultHolder<GetBucketResponse>>(
            "buckets", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetBucketResponse>> UpdateBucket(string bucketCode,
        UpdateBucketRequest request)
    {
        return await PutAsync<UpdateBucketRequest, CustomResultHolder<GetBucketResponse>>(
            $"buckets/{bucketCode}", request, ApiVersion1_2);
    }

    public async Task DeleteBucket(string bucketCode)
    {
        await DeleteAsync($"buckets/{bucketCode}", ApiVersion1_2);
    }

    // ── Portfolios ─────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<GetPortfolio>> CreatePortfolio(CreatePortfolioRequest request)
    {
        return await PostAsync<CreatePortfolioRequest, CustomResultHolder<GetPortfolio>>(
            "portfolios", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetPortfolio>> UpdatePortfolio(string portfolioCode,
        UpdatePortfolioRequest request)
    {
        return await PutAsync<UpdatePortfolioRequest, CustomResultHolder<GetPortfolio>>(
            $"portfolios/{portfolioCode}", request, ApiVersion1_2);
    }

    // ── Payment methods ────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<PaymentMethodResponse>>> GetPaymentMethods(
        GetPaymentMethodsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<PaymentMethodResponse>>>(
            $"paymentmethods?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PaymentMethodResponse>> GetPaymentMethod(string paymentMethodCode)
    {
        return await GetAsync<CustomResultHolder<PaymentMethodResponse>>(
            $"paymentmethods/{paymentMethodCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<PaymentMethodCustomerFeeResponse>>> GetPaymentMethodCustomerFees(
        string paymentMethodCode, GetPaymentMethodCustomerFeesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<PaymentMethodCustomerFeeResponse>>>(
            $"paymentmethods/{paymentMethodCode}/fees/customer?{QueryParameterHelper.ToQueryString(request)}",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PaymentMethodCustomerFeeResponse>> UpsertPaymentMethodCustomerFee(
        string paymentMethodCode, UpsertPaymentMethodCustomerFeeRequest request)
    {
        return await PutAsync<UpsertPaymentMethodCustomerFeeRequest, CustomResultHolder<PaymentMethodCustomerFeeResponse>>(
            $"paymentmethods/{paymentMethodCode}/fees/customer", request, ApiVersion1_2);
    }

    public async Task DeletePaymentMethodCustomerFee(string paymentMethodCode, string customerCode)
    {
        await DeleteAsync($"paymentmethods/{paymentMethodCode}/fees/customer/{customerCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<PaymentMethodTagFeeResponse>>> GetPaymentMethodTagFees(
        string paymentMethodCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<PaymentMethodTagFeeResponse>>>(
            $"paymentmethods/{paymentMethodCode}/fees/tag", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PaymentMethodTagFeeResponse>> UpsertPaymentMethodTagFee(
        string paymentMethodCode, UpsertPaymentMethodTagFeeRequest request)
    {
        return await PutAsync<UpsertPaymentMethodTagFeeRequest, CustomResultHolder<PaymentMethodTagFeeResponse>>(
            $"paymentmethods/{paymentMethodCode}/fees/tag", request, ApiVersion1_2);
    }

    public async Task DeletePaymentMethodTagFee(string paymentMethodCode, string customerTag)
    {
        await DeleteAsync($"paymentmethods/{paymentMethodCode}/fees/tag/{customerTag}", ApiVersion1_2);
    }

    // ── Crypto addresses ───────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetCryptoAddressResponse>>> GetCryptoAddresses(
        GetCryptoAddressesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCryptoAddressResponse>>>(
            $"cryptoaddress?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    // ── Exchanges & trade pairs ────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<ExchangeModel>>> GetExchanges(
        GetExchangesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<ExchangeModel>>>(
            $"exchanges?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<TradePairItem>>> GetTradePairs(
        GetTradePairsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<TradePairItem>>>(
            $"tradepairs?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    // ── Transfers ──────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<GetTransfer>> CreateTransfer(CreateTransferRequest request)
    {
        return await PostAsync<CreateTransferRequest, CustomResultHolder<GetTransfer>>(
            "transfers", request, ApiVersion1_2);
    }

    // ── Orders ─────────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<ListOrder>> CreateOrder(CreateOrderRequest request)
    {
        return await PostAsync<CreateOrderRequest, CustomResultHolder<ListOrder>>(
            "orders", request, ApiVersion1_2);
    }

    public async Task DeleteOrder(string orderCode)
    {
        await DeleteAsync($"orders/{orderCode}", ApiVersion1_2);
    }

    // ── Mails ──────────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<GetMail>> GetMail(string mailCode)
    {
        return await GetAsync<CustomResultHolder<GetMail>>($"mail/{mailCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetMail>> CreateMail(PostMailRequest request)
    {
        return await PostAsync<PostMailRequest, CustomResultHolder<GetMail>>("mail", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetMail>> UpdateMail(string mailCode, PutMailRequest request)
    {
        return await PutAsync<PutMailRequest, CustomResultHolder<GetMail>>(
            $"mail/{mailCode}", request, ApiVersion1_2);
    }

    // ── Notifications ──────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetNotificationResponse>>> GetNotifications(
        GetNotificationsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetNotificationResponse>>>(
            $"notifications?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task CreateNotification(CreateNotificationRequest request)
    {
        await PostAsync<CreateNotificationRequest, CustomResultHolder<object>>(
            "notifications", request, ApiVersion1_2);
    }

    // ── Banks ──────────────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<BankModel>>> GetBanks(GetBanksRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<BankModel>>>(
            $"banks?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<BankModel>> GetBank(Guid bankId)
    {
        return await GetAsync<CustomResultHolder<BankModel>>($"banks/{bankId}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<BankModel>> CreateBank(CreateBankRequest request)
    {
        return await PostAsync<CreateBankRequest, CustomResultHolder<BankModel>>("banks", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<BankModel>> UpdateBank(Guid bankId, UpdateBankRequest request)
    {
        return await PutAsync<UpdateBankRequest, CustomResultHolder<BankModel>>(
            $"banks/{bankId}", request, ApiVersion1_2);
    }

    public async Task DeleteBank(Guid bankId)
    {
        await DeleteAsync($"banks/{bankId}", ApiVersion1_2);
    }

    // ── Customer bank accounts (additional) ───────────────────────────────────

    public async Task<CustomResultHolder<CustomerBankAccountResponse>> GetCustomerBankAccount(
        string customerCode, Guid bankAccountId)
    {
        return await GetAsync<CustomResultHolder<CustomerBankAccountResponse>>(
            $"customer/{customerCode}/bankaccounts/{bankAccountId}", ApiVersion1_2);
    }

    // ── Customer limits ────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<GetBrokerLimitResponse>> GetCustomerBuyLimit(
        GetCustomerLimitRequest request)
    {
        return await GetAsync<CustomResultHolder<GetBrokerLimitResponse>>(
            $"customer/{request.CustomerCode}/limits/buy?paymentMethodCode={request.PaymentMethodCode}&cryptoCode={request.CryptoCode}",
            ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetBrokerLimitResponse>> GetCustomerSellLimit(
        GetCustomerLimitRequest request)
    {
        return await GetAsync<CustomResultHolder<GetBrokerLimitResponse>>(
            $"customer/{request.CustomerCode}/limits/sell?paymentMethodCode={request.PaymentMethodCode}&cryptoCode={request.CryptoCode}",
            ApiVersion1_2);
    }

    // ── Customer comments ──────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetCommentsResponse>>> GetCustomerComments(
        string customerCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCommentsResponse>>>(
            $"customer/{customerCode}/comments", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetCommentsResponse>> AddCustomerComment(string customerCode,
        AddCommentRequest request)
    {
        return await PostAsync<AddCommentRequest, CustomResultHolder<GetCommentsResponse>>(
            $"customer/{customerCode}/comments", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetCommentsResponse>> UpdateCustomerComment(string customerCode,
        Guid commentId, UpdateCommentRequest request)
    {
        return await PutAsync<UpdateCommentRequest, CustomResultHolder<GetCommentsResponse>>(
            $"customer/{customerCode}/comments/{commentId}", request, ApiVersion1_2);
    }

    public async Task DeleteCustomerComment(string customerCode, Guid commentId)
    {
        await DeleteAsync($"customer/{customerCode}/comments/{commentId}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<PagedResult<GetCommentHistoryResponse>>> GetCustomerCommentHistory(
        string customerCode, Guid commentId)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCommentHistoryResponse>>>(
            $"customer/{customerCode}/comments/{commentId}/history", ApiVersion1_2);
    }

    // ── Customer compliance history ────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetCustomerComplianceHistoryResponse>>> GetCustomerComplianceHistory(
        string customerCode, GetCustomerComplianceHistoryRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetCustomerComplianceHistoryResponse>>>(
            $"customer/{customerCode}/compliancehistory?{QueryParameterHelper.ToQueryString(request)}",
            ApiVersion1_2);
    }

    // ── Customer tags ──────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<CustomerTagResponse>>> GetCustomerTags(string customerCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<CustomerTagResponse>>>(
            $"customer/{customerCode}/tags", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustomerTagResponse>> AddCustomerTag(string customerCode,
        CreateCustomerTagRequest request)
    {
        return await PostAsync<CreateCustomerTagRequest, CustomResultHolder<CustomerTagResponse>>(
            $"customer/{customerCode}/tags", request, ApiVersion1_2);
    }

    public async Task DeleteCustomerTag(string customerCode, string tag)
    {
        await DeleteAsync($"customer/{customerCode}/tags/{tag}", ApiVersion1_2);
    }

    // ── Customer data ──────────────────────────────────────────────────────────

    public async Task<CustomResultHolder<IDictionary<string, string>>> GetCustomerData(string customerCode)
    {
        return await GetAsync<CustomResultHolder<IDictionary<string, string>>>(
            $"customer/{customerCode}/data", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<IDictionary<string, string>>> UpsertCustomerData(string customerCode,
        UpsertCustomerDataRequest request)
    {
        return await PutAsync<UpsertCustomerDataRequest, CustomResultHolder<IDictionary<string, string>>>(
            $"customer/{customerCode}/data", request, ApiVersion1_2);
    }

    public async Task DeleteCustomerDataKey(string customerCode, string key)
    {
        await DeleteAsync($"customer/{customerCode}/data/{key}", ApiVersion1_2);
    }

    // ── Broker: buy / sell ─────────────────────────────────────────────────────

    public async Task<CustomResultHolder<BuyResponse>> Buy(BuyRequest request)
    {
        return await PostAsync<BuyRequest, CustomResultHolder<BuyResponse>>("buy", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<BuySimulateResponse>> SimulateBuy(BuySimulateRequest request)
    {
        return await PostAsync<BuySimulateRequest, CustomResultHolder<BuySimulateResponse>>(
            "buy/simulate", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<SellResponse>> Sell(SellRequest request)
    {
        return await PostAsync<SellRequest, CustomResultHolder<SellResponse>>("sell", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<SellSimulateResponse>> SimulateSell(SellSimulateRequest request)
    {
        return await PostAsync<SellSimulateRequest, CustomResultHolder<SellSimulateResponse>>(
            "sell/simulate", request, ApiVersion1_2);
    }

    // ── Broker: transaction extras ─────────────────────────────────────────────

    public async Task<CustomResultHolder<GetTransaction>> UpdateBrokerTransaction(string txCode,
        UpdateBrokerTransactionRequest request)
    {
        return await PutAsync<UpdateBrokerTransactionRequest, CustomResultHolder<GetTransaction>>(
            $"transaction/{txCode}", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetTransaction>> ExecuteTransaction(ExecuteTransactionRequest request)
    {
        return await PostAsync<ExecuteTransactionRequest, CustomResultHolder<GetTransaction>>(
            "transaction/execute", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetTransactionFlowResult>> GetTransactionFlow(string txCode)
    {
        return await GetAsync<CustomResultHolder<GetTransactionFlowResult>>(
            $"transaction/{txCode}/flow", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<IEnumerable<GetTransactionDataModel>>> GetTransactionData(string txCode)
    {
        return await GetAsync<CustomResultHolder<IEnumerable<GetTransactionDataModel>>>(
            $"transaction/{txCode}/data", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetTransactionDataModel>> PostTransactionData(string txCode, string key,
        PostTransactionDataModel request)
    {
        return await PostAsync<PostTransactionDataModel, CustomResultHolder<GetTransactionDataModel>>(
            $"transaction/{txCode}/data/{key}", request, ApiVersion1_2);
    }

    // ── Broker: merchant ──────────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetMerchantTransactionResponse>>> GetMerchantTransactions(
        GetMerchantTransactionsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetMerchantTransactionResponse>>>(
            $"merchant?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetMerchantTransactionResponse>> GetMerchantTransaction(string txCode)
    {
        return await GetAsync<CustomResultHolder<GetMerchantTransactionResponse>>(
            $"merchant/{txCode}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetMerchantTransactionResponse>> CreateMerchantTransaction(
        CreateMerchantTransactionRequest request)
    {
        return await PostAsync<CreateMerchantTransactionRequest, CustomResultHolder<GetMerchantTransactionResponse>>(
            "merchant", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetMerchantTransactionResponse>> UpdateMerchantTransaction(string txCode,
        UpdateMerchantTransactionRequest request)
    {
        return await PutAsync<UpdateMerchantTransactionRequest, CustomResultHolder<GetMerchantTransactionResponse>>(
            $"merchant/{txCode}", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<SimulateMerchantTransactionResponse>> SimulateMerchantTransaction(
        CreateMerchantTransactionRequest request)
    {
        return await PostAsync<CreateMerchantTransactionRequest, CustomResultHolder<SimulateMerchantTransactionResponse>>(
            "merchant/simulate", request, ApiVersion1_2);
    }

    // ── Custodian: transactions ────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<ListCustodianTransactionResponse>>> GetCustodianTransactions(
        GetCustodianTransactionsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<ListCustodianTransactionResponse>>>(
            $"transactions/custodian?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianBuyResponse>> CustodianBuy(CustodianBuyRequest request)
    {
        return await PostAsync<CustodianBuyRequest, CustomResultHolder<CustodianBuyResponse>>(
            "transactions/custodian/buy", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSellResponse>> CustodianSell(CustodianSellRequest request)
    {
        return await PostAsync<CustodianSellRequest, CustomResultHolder<CustodianSellResponse>>(
            "transactions/custodian/sell", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianGiftResponse>> CustodianGift(CustodianGiftRequest request)
    {
        return await PostAsync<CustodianGiftRequest, CustomResultHolder<CustodianGiftResponse>>(
            "transactions/custodian/gift", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianClawbackResponse>> CustodianClawback(
        CustodianClawbackRequest request)
    {
        return await PostAsync<CustodianClawbackRequest, CustomResultHolder<CustodianClawbackResponse>>(
            "transactions/custodian/clawback", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSendInternalResponse>> CustodianSendInternal(
        CustodianSendInternalRequest request)
    {
        return await PostAsync<CustodianSendInternalRequest, CustomResultHolder<CustodianSendInternalResponse>>(
            "transactions/custodian/sendinternal", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSendOutResponse>> CustodianSendOut(CustodianSendOutRequest request)
    {
        return await PostAsync<CustodianSendOutRequest, CustomResultHolder<CustodianSendOutResponse>>(
            "transactions/custodian/sendout", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSendOutResponse>> CustodianSendToBucket(
        CustodianSendToBucketRequest request)
    {
        return await PostAsync<CustodianSendToBucketRequest, CustomResultHolder<CustodianSendOutResponse>>(
            "transactions/custodian/sendtobucket", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSwapResponse>> CustodianSwap(CustodianSwapRequest request)
    {
        return await PostAsync<CustodianSwapRequest, CustomResultHolder<CustodianSwapResponse>>(
            "transactions/custodian/swap", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianSwapSimulationResponse>> SimulateCustodianSwap(
        CustodianSwapSimulateRequest request)
    {
        return await PostAsync<CustodianSwapSimulateRequest, CustomResultHolder<CustodianSwapSimulationResponse>>(
            "transactions/custodian/swap/simulate", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<CustodianPaymentRequestResponse>> CustodianPaymentRequest(
        CustodianPaymentRequest request)
    {
        return await PostAsync<CustodianPaymentRequest, CustomResultHolder<CustodianPaymentRequestResponse>>(
            "transactions/custodian/payment", request, ApiVersion1_2);
    }

    // ── Custodian: callbacks ───────────────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<CustodianTransactionNotificationCallbackResponse>>> GetCustodianCallbacks(
        string txCode)
    {
        return await GetAsync<CustomResultHolder<PagedResult<CustodianTransactionNotificationCallbackResponse>>>(
            $"transactions/custodian/{txCode}/callbacks", ApiVersion1_2);
    }

    // ── Custodian: interest schedules ──────────────────────────────────────────

    public async Task<CustomResultHolder<PagedResult<GetScheduleResponse>>> GetInterestSchedules(
        GetSchedulesRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<PagedResult<GetScheduleResponse>>>(
            $"schedules?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetScheduleWithIntervalsResponse>> GetInterestSchedule(Guid scheduleId)
    {
        return await GetAsync<CustomResultHolder<GetScheduleWithIntervalsResponse>>(
            $"schedules/{scheduleId}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetScheduleResponse>> CreateInterestSchedule(
        CreateCustodianScheduleRequest request)
    {
        return await PostAsync<CreateCustodianScheduleRequest, CustomResultHolder<GetScheduleResponse>>(
            "schedules", request, ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetScheduleResponse>> UpdateInterestSchedule(Guid scheduleId,
        EditCustodianScheduleRequest request)
    {
        return await PutAsync<EditCustodianScheduleRequest, CustomResultHolder<GetScheduleResponse>>(
            $"schedules/{scheduleId}", request, ApiVersion1_2);
    }

    public async Task DeleteInterestSchedule(Guid scheduleId)
    {
        await DeleteAsync($"schedules/{scheduleId}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetScheduleIntervalResponse>> GetInterestScheduleInterval(Guid scheduleId,
        GetScheduleIntervalsRequest? request = null)
    {
        return await GetAsync<CustomResultHolder<GetScheduleIntervalResponse>>(
            $"schedules/{scheduleId}/intervals?{QueryParameterHelper.ToQueryString(request)}", ApiVersion1_2);
    }

    public async Task<CustomResultHolder<GetScheduleSubIntervalResponse>> UpdateInterestScheduleSubInterval(
        Guid scheduleId, Guid intervalId, Guid subIntervalId, EditCustodianSubIntervalRequest request)
    {
        return await PutAsync<EditCustodianSubIntervalRequest, CustomResultHolder<GetScheduleSubIntervalResponse>>(
            $"schedules/{scheduleId}/intervals/{intervalId}/subintervals/{subIntervalId}", request, ApiVersion1_2);
    }
}