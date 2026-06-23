using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Account;
using Nexus.Crypto.SDK.Models.Broker;
using Nexus.Crypto.SDK.Models.Custodian;
using Nexus.Crypto.SDK.Models.PriceChartModel;
using Nexus.Crypto.SDK.Models.Response;
using Nexus.Crypto.SDK.Services;

namespace Nexus.Crypto.SDK;

public interface INexusAPIService
{
    IDocumentStoreService DocumentStore { get; }
    ICustomerService Customer { get; }
    ICustomerPersonService CustomerPerson { get; }

    // Label partner
    Task<CustomResultHolder<GetLabelPartner>> GetLabelPartner();
    Task<CustomResultHolder<LabelPartnerResponse>> GetLabelPartnerConfig();
    Task<CustomResultHolder<PagedResult<LabelPartnerCryptoCurrency>>> GetLabelPartnerCryptoCurrencies(GetLabelPartnerCryptoCurrenciesRequest? request = null);
    Task<CustomResultHolder<PagedResult<LabelPartnerCountry>>> GetLabelPartnerCountries(GetLabelPartnerCountriesRequest? request = null);

    // Currencies
    Task<CustomResultHolder<GetCurrencies>> GetCurrencies();

    // Prices
    Task<CustomResultHolder<GetPrices>> GetPrices(string currency);
    Task<CustomResultHolder<GetPriceData>> GetHistoricPrices(string currency, string crypto, GetPriceDataRequest? request = null);

    // Reserves
    Task<CustomResultHolder<GetReserves>> GetReserves(string reservesTimeStamp);

    // Balances
    Task<CustomResultHolder<GetCustodianBalances>> GetCustodianBalances();
    Task<CustomResultHolder<GetBrokerBalances_1_1>> GetBrokerBalances();
    Task<CustomResultHolder<PagedResult<HotWalletBalance>>> GetHotWalletBalances(GetHotWalletBalancesRequest? request = null);
    Task<CustomResultHolder<PagedResult<GetBalanceMutation>>> GetBalanceMutations(Dictionary<string, string> queryParams);

    // Accounts
    Task<CustomResultHolder<PagedResult<GetAccountResponse>>> GetAccounts(GetAccountsRequest? request = null);
    Task<CustomResultHolder<GetAccountResponse>> GetAccount(string accountCode);
    Task<CustomResultHolder<CreateAccountResponse>> CreateAccount(string customerCode, CreateAccountRequest request);
    Task<CustomResultHolder<GetAccountResponse>> UpdateAccount(string accountCode, UpdateAccountRequest request);
    Task<CustomResultHolder<CreateAccountResponse>> CreateNonNativeAccount(string accountCode, CreateNonNativeAccountRequest request);
    Task<CustomResultHolder<PagedResult<GetAccountBalanceResponse>>> GetAccountBalances(string accountCode);

    // Buckets
    Task<CustomResultHolder<PagedResult<GetBucketResponse>>> GetBuckets(GetBucketsRequest? request = null);
    Task<CustomResultHolder<GetBucketResponse>> GetBucket(string bucketCode);
    Task<CustomResultHolder<GetBucketResponse>> CreateBucket(CreateBucketRequest request);
    Task<CustomResultHolder<GetBucketResponse>> UpdateBucket(string bucketCode, UpdateBucketRequest request);
    Task DeleteBucket(string bucketCode);

    // Portfolios
    Task<CustomResultHolder<List<GetPortfolio>>> GetPortfolios();
    Task<CustomResultHolder<GetPortfolio>> CreatePortfolio(CreatePortfolioRequest request);
    Task<CustomResultHolder<GetPortfolio>> UpdatePortfolio(string portfolioCode, UpdatePortfolioRequest request);

    // Trust levels
    Task<CustomResultHolder<PagedResult<GetTrustLevel>>> GetTrustLevels();

    // Payment methods
    Task<CustomResultHolder<PagedResult<PaymentMethodResponse>>> GetPaymentMethods(GetPaymentMethodsRequest? request = null);
    Task<CustomResultHolder<PaymentMethodResponse>> GetPaymentMethod(string paymentMethodCode);
    Task<CustomResultHolder<PagedResult<PaymentMethodCustomerFeeResponse>>> GetPaymentMethodCustomerFees(string paymentMethodCode, GetPaymentMethodCustomerFeesRequest? request = null);
    Task<CustomResultHolder<PaymentMethodCustomerFeeResponse>> UpsertPaymentMethodCustomerFee(string paymentMethodCode, UpsertPaymentMethodCustomerFeeRequest request);
    Task DeletePaymentMethodCustomerFee(string paymentMethodCode, string customerCode);
    Task<CustomResultHolder<PagedResult<PaymentMethodTagFeeResponse>>> GetPaymentMethodTagFees(string paymentMethodCode);
    Task<CustomResultHolder<PaymentMethodTagFeeResponse>> UpsertPaymentMethodTagFee(string paymentMethodCode, UpsertPaymentMethodTagFeeRequest request);
    Task DeletePaymentMethodTagFee(string paymentMethodCode, string customerTag);

    // Crypto addresses
    Task<CustomResultHolder<PagedResult<GetCryptoAddressResponse>>> GetCryptoAddresses(GetCryptoAddressesRequest? request = null);

    // Exchanges & trade pairs
    Task<CustomResultHolder<PagedResult<ExchangeModel>>> GetExchanges(GetExchangesRequest? request = null);
    Task<CustomResultHolder<PagedResult<TradePairItem>>> GetTradePairs(GetTradePairsRequest? request = null);

    // Transfers
    Task<CustomResultHolder<PagedResult<GetTransfer>>> GetTransfers(GetTransfersRequest? request = null);
    Task<CustomResultHolder<GetTransfer>> CreateTransfer(CreateTransferRequest request);

    // Orders
    Task<CustomResultHolder<PagedResult<ListOrder>>> GetOrders(GetOrdersRequest? request = null);
    Task<CustomResultHolder<ListOrder>> CreateOrder(CreateOrderRequest request);
    Task DeleteOrder(string orderCode);

    // Mails
    Task<CustomResultHolder<PagedResult<GetMail>>> GetMails(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<GetMail>> GetMail(string mailCode);
    Task<CustomResultHolder<GetMail>> CreateMail(PostMailRequest request);
    Task<CustomResultHolder<GetMail>> UpdateMail(string mailCode, PutMailRequest request);

    // Notifications
    Task<CustomResultHolder<PagedResult<GetNotificationResponse>>> GetNotifications(GetNotificationsRequest? request = null);
    Task CreateNotification(CreateNotificationRequest request);

    // Callbacks
    Task<CustomResultHolder<PagedResult<TransactionNotificationCallbackResponse>>> GetCallbacks(string transactionCode);

    // Bank accounts
    Task<CustomResultHolder<PagedResult<CustomerBankAccountResponse>>> GetCustomerBankAccounts(string customerCode, Dictionary<string, string> queryParams);
    Task<CustomResultHolder<CustomerBankAccountResponse>> GetCustomerBankAccount(string customerCode, Guid bankAccountId);
    Task<CustomResultHolder<CustomerBankAccountResponse>> CreateCustomerBankAccount(string customerCode, CreateBankAccountRequestModel request);
    Task DeleteCustomerBankAccount(string customerCode, Guid bankAccountId);
    Task<CustomResultHolder<CustomerBankAccountResponse>> UpdateCustomerBankAccount(string customerCode, Guid bankAccountId, UpdateBankAccountRequest request);

    // Banks
    Task<CustomResultHolder<PagedResult<BankModel>>> GetBanks(GetBanksRequest? request = null);
    Task<CustomResultHolder<BankModel>> GetBank(Guid bankId);
    Task<CustomResultHolder<BankModel>> CreateBank(CreateBankRequest request);
    Task<CustomResultHolder<BankModel>> UpdateBank(Guid bankId, UpdateBankRequest request);
    Task DeleteBank(Guid bankId);

    // Chart prices
    Task<IEnumerable<ChartSeriesModelPT>> GetMinutePrices(int timeSpan, string currencyCode, string cryptoCode);

    // Customer limits
    Task<CustomResultHolder<GetBrokerLimitResponse>> GetCustomerBuyLimit(GetCustomerLimitRequest request);
    Task<CustomResultHolder<GetBrokerLimitResponse>> GetCustomerSellLimit(GetCustomerLimitRequest request);

    // Customer comments
    Task<CustomResultHolder<PagedResult<GetCommentsResponse>>> GetCustomerComments(string customerCode);
    Task<CustomResultHolder<GetCommentsResponse>> AddCustomerComment(string customerCode, AddCommentRequest request);
    Task<CustomResultHolder<GetCommentsResponse>> UpdateCustomerComment(string customerCode, Guid commentId, UpdateCommentRequest request);
    Task DeleteCustomerComment(string customerCode, Guid commentId);
    Task<CustomResultHolder<PagedResult<GetCommentHistoryResponse>>> GetCustomerCommentHistory(string customerCode, Guid commentId);

    // Customer compliance history
    Task<CustomResultHolder<PagedResult<GetCustomerComplianceHistoryResponse>>> GetCustomerComplianceHistory(string customerCode, GetCustomerComplianceHistoryRequest? request = null);

    // Customer tags
    Task<CustomResultHolder<PagedResult<CustomerTagResponse>>> GetCustomerTags(string customerCode);
    Task<CustomResultHolder<CustomerTagResponse>> AddCustomerTag(string customerCode, CreateCustomerTagRequest request);
    Task DeleteCustomerTag(string customerCode, string tag);

    // Customer data
    Task<CustomResultHolder<IDictionary<string, string>>> GetCustomerData(string customerCode);
    Task<CustomResultHolder<IDictionary<string, string>>> UpsertCustomerData(string customerCode, UpsertCustomerDataRequest request);
    Task DeleteCustomerDataKey(string customerCode, string key);
}

public interface INexusBrokerAPIService : INexusAPIService
{
    // Broker transactions
    Task<CustomResultHolder<GetTransaction>> GetBrokerTransaction(string txCode);
    Task<CustomResultHolder<PagedResult<GetTransaction>>> GetBrokerTransactions(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<TotalsResult<TransactionTotals>>> GetBrokerTransactionTotals(Dictionary<string, string> queryParams);
    Task<CustomResultHolder<GetTransaction>> UpdateBrokerTransaction(string txCode, UpdateBrokerTransactionRequest request);
    Task<CustomResultHolder<GetTransaction>> ExecuteTransaction(ExecuteTransactionRequest request);
    Task<CustomResultHolder<GetTransactionFlowResult>> GetTransactionFlow(string txCode);
    Task<CustomResultHolder<IEnumerable<GetTransactionDataModel>>> GetTransactionData(string txCode);
    Task<CustomResultHolder<GetTransactionDataModel>> PostTransactionData(string txCode, string key, PostTransactionDataModel request);

    // Buy / Sell
    Task<CustomResultHolder<BuyResponse>> Buy(BuyRequest request);
    Task<CustomResultHolder<BuySimulateResponse>> SimulateBuy(BuySimulateRequest request);
    Task<CustomResultHolder<SellResponse>> Sell(SellRequest request);
    Task<CustomResultHolder<SellSimulateResponse>> SimulateSell(SellSimulateRequest request);

    // Merchant
    Task<CustomResultHolder<PagedResult<GetMerchantTransactionResponse>>> GetMerchantTransactions(GetMerchantTransactionsRequest? request = null);
    Task<CustomResultHolder<GetMerchantTransactionResponse>> GetMerchantTransaction(string txCode);
    Task<CustomResultHolder<GetMerchantTransactionResponse>> CreateMerchantTransaction(CreateMerchantTransactionRequest request);
    Task<CustomResultHolder<GetMerchantTransactionResponse>> UpdateMerchantTransaction(string txCode, UpdateMerchantTransactionRequest request);
    Task<CustomResultHolder<SimulateMerchantTransactionResponse>> SimulateMerchantTransaction(CreateMerchantTransactionRequest request);
}

public interface INexusCustodianAPIService : INexusAPIService
{
    // Custodian transactions
    Task<CustomResultHolder<ListCustodianTransactionResponse>> GetCustodianTransaction(string txCode);
    Task<CustomResultHolder<PagedResult<ListCustodianTransactionResponse>>> GetCustodianTransactions(GetCustodianTransactionsRequest? request = null);
    Task<CustomResultHolder<CustodianCancelResponse>> CancelCustodianTransaction(string txCode);

    // Custodian transaction types
    Task<CustomResultHolder<CustodianBuyResponse>> CustodianBuy(CustodianBuyRequest request);
    Task<CustomResultHolder<CustodianSellResponse>> CustodianSell(CustodianSellRequest request);
    Task<CustomResultHolder<CustodianGiftResponse>> CustodianGift(CustodianGiftRequest request);
    Task<CustomResultHolder<CustodianClawbackResponse>> CustodianClawback(CustodianClawbackRequest request);
    Task<CustomResultHolder<CustodianSendInternalResponse>> CustodianSendInternal(CustodianSendInternalRequest request);
    Task<CustomResultHolder<CustodianSendOutResponse>> CustodianSendOut(CustodianSendOutRequest request);
    Task<CustomResultHolder<CustodianSendOutResponse>> CustodianSendToBucket(CustodianSendToBucketRequest request);
    Task<CustomResultHolder<CustodianSwapResponse>> CustodianSwap(CustodianSwapRequest request);
    Task<CustomResultHolder<CustodianSwapSimulationResponse>> SimulateCustodianSwap(CustodianSwapSimulateRequest request);
    Task<CustomResultHolder<CustodianPaymentRequestResponse>> CustodianPaymentRequest(CustodianPaymentRequest request);

    // Custodian callbacks
    Task<CustomResultHolder<PagedResult<CustodianTransactionNotificationCallbackResponse>>> GetCustodianCallbacks(string txCode);

    // Interest schedules
    Task<CustomResultHolder<PagedResult<GetScheduleResponse>>> GetInterestSchedules(GetSchedulesRequest? request = null);
    Task<CustomResultHolder<GetScheduleWithIntervalsResponse>> GetInterestSchedule(Guid scheduleId);
    Task<CustomResultHolder<GetScheduleResponse>> CreateInterestSchedule(CreateCustodianScheduleRequest request);
    Task<CustomResultHolder<GetScheduleResponse>> UpdateInterestSchedule(Guid scheduleId, EditCustodianScheduleRequest request);
    Task DeleteInterestSchedule(Guid scheduleId);
    Task<CustomResultHolder<GetScheduleIntervalResponse>> GetInterestScheduleInterval(Guid scheduleId, GetScheduleIntervalsRequest? request = null);
    Task<CustomResultHolder<GetScheduleSubIntervalResponse>> UpdateInterestScheduleSubInterval(Guid scheduleId, Guid intervalId, Guid subIntervalId, EditCustodianSubIntervalRequest request);
}
