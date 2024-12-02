namespace Nexus.LabelApi.SDK.Models
{
    public class GetTransfer
    {
        public string TransferCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Created { get; set; }
        public string Finished { get; set; }
        public string ExchangeTransferCode { get; set; }
        public decimal? Price { get; set; }
        public string TxId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public string CryptoCode { get; set; }
        public GetTransferAddress Address { get; set; }
    }

    public class GetTransferAddress
    {
        public string SourceType { get; set; }
        public string SinkType { get; set; }
        public string Address { get; set; }
        public string SinkExchangeCode { get; set; }
        public string SourceExchangeCode { get; set; }
    }

}
