using System;
using System.Collections.Generic;

namespace Nexus.LabelApi.SDK.Models;

public class GetPrices
{
    public string Created { get; set; }
    public string CurrencyCode { get; set; }
    public IDictionary<string, Prices> Prices { get; set; }
}
public class Prices
{
    public double? Buy { get; set; }
    public double? Sell { get; set; }
    public double? EstimatedNetworkSlowFee { get; set; }
    public double? EstimatedNetworkFastFee { get; set; }
    public DateTime? Updated { get; set; }
}
