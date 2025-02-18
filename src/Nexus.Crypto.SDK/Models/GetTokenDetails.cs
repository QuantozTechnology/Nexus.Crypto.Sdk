using System.Collections.Generic;

namespace Nexus.Crypto.SDK.Models;

public class GetTokenDetails
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string IssuerAddress { get; set; }
    public string Status { get; set; }
    public string TokenType { get; set; }
    public string AssetType { get; set; }
    public string OwnerType { get; set; }
    public string Created { get; set; }
    public Limits Limits { get; set; }
    public Dictionary<string, bool> Flags { get; set; }
    public PeggedBy PeggedBy { get; set; }
    public PeggedBy RootPeggedBy { get; set; }
    public PeggedBy BasePeggedBy { get; set; }
}

public class Limits
{
    public long? AccountLimit { get; set; }
    public long? OverallLimit { get; set; }
}
