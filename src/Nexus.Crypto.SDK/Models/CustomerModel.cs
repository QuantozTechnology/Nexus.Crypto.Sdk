using System.Collections.Generic;

namespace Nexus.Crypto.SDK.Models;

public class GetCustomer
{
    public string CustomerCode { get; set; }

    public string Email { get; set; }

    public string Created { get; set; }

    public string BankAccount { get; set; }

    public string BankAccountName { get; set; }

    public string Status { get; set; }

    public string Trustlevel { get; set; }

    public string PortFolioCode { get; set; }

    public bool IsHighRisk { get; set; }

    public bool IsBusiness { get; set; }

    public bool HasPhotoId { get; set; }

    public string CountryCode { get; set; }

    public string Comment { get; set; }

    public IDictionary<string, string> Data { get; set; }
}
