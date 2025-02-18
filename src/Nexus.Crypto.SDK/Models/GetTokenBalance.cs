namespace Nexus.Crypto.SDK.Models;

public class GetTokenBalance
{
    public string TokenCode { get; set; }
    public decimal Issued { get; set; }
    public decimal Deleted { get; set; }
    public decimal Available { get; set; }
    public decimal Pegged { get; set; }
    public decimal Total { get; set; }
    public string Updated { get; set; }
}
