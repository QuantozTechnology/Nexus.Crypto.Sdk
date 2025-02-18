using System;
using System.Collections.Generic;
using System.Text;

namespace Nexus.LabelApi.SDK.Models;

public class GetBalanceMutation
{
    /// <summary>
    /// Type of mutation
    /// </summary>
    /// <remarks>
    /// BUY | SELL | GIFT | DEPOSIT | WITHDRAW | FEE | CORRECTION | RECEIVEIN | SENDOUT
    /// </remarks>
    public string Type { get; set; }
    /// <summary>
    /// Creation date of the mutation
    /// </summary>
    public string Created { get; set; }
    /// <summary>
    /// Code of transaction initiating this mutation
    /// </summary>
    public string TransactionCode { get; set; }
    /// <summary>
    /// Mutation amount in crypto
    /// </summary>
    public decimal CryptoAmount { get; set; }
    /// <summary>
    /// Mutation crypto currency
    /// </summary>
    public string CryptoCode { get; set; }
    /// <summary>
    /// Fiat value of this mutation
    /// </summary>
    public decimal Value { get; set; }
}
