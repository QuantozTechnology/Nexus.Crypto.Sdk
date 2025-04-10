namespace Nexus.Crypto.SDK.Models;

public class GetTransferRequest
{
    public string CreatedFrom { get; set; }
    public string CreatedTill { get; set; }
    public string FinishedFrom { get; set; }
    public string FinishedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string SortBy { get; set; }
    public string Status { get; set; }
    public string SourceType { get; set; }
    public string SinkType { get; set; }
    public string SourceExchangeCode { get; set; }
    public string SinkExchangeCode { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public enum SortDirection
{
    Asc,
    Desc
}

public class GetTransfer
{
    public string TransferCode { get; set; }
    public TransferType Type { get; set; }
    public TransferStatus Status { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Finished { get; set; }
    public string? ExchangeTransferCode { get; set; }
    public decimal? Price { get; set; }
    public string? TxId { get; set; }
    public string? UserName { get; set; }
    public string? Comment { get; set; }
    public string CryptoCode { get; set; }
    public GetTransferAddress Address { get; set; }
}

public class GetTransferAddress
{
    public string SourceType { get; set; }
    public string SinkType { get; set; }
    public string Address { get; set; }
    public string? SinkExchangeCode { get; set; }
    public string? SourceExchangeCode { get; set; }
}

public enum TransferType
{
    Withdraw,
    Deposit
}

public enum TransferStatus
{
    Initiated,
    Completed,
    Failed,
    Cancelled,
    ToCancel
}

public enum TransferAddressType
{
    Exchange,
    HotWallet,
    Coldstore,
    Other
}