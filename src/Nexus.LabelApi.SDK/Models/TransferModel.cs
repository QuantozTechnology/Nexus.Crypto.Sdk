namespace FrontendAPI.Models
{
    public class CreateTransferModel
    {
        public string SourceType { get; set; }
        public string SinkType { get; set; }
        public string SourceExchangeCode { get; set; }
        public string SinkExchangeCode { get; set; }
        public decimal Amount { get; set; }
        public string CryptoCode { get; set; }
        public string TransferAddress { get; set; }
        public string UserName { get; set; }
    }
}
