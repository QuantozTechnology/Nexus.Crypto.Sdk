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

public class CreateCustomerResponse
{
    public string CustomerCode { get; set; }
    public string Created { get; set; }
    public string Status { get; set; }
    public string Trustlevel { get; set; }
    public string PortFolioCode { get; set; }
    public string CurrencyCode { get; set; }
    public bool IsBusiness { get; set; }
}

public class CreateCustomerRequest
{
    public string CustomerCode { get; set; }
    public string PortfolioCode { get; set; }
    public string TrustLevel { get; set; }
    public string Status { get; set; }
    public string CurrencyCode { get; set; }
    public bool IsBusiness { get; set; } = false;
}

public class DeleteCustomerResponse
{
    public string CustomerCode { get; set; }
    public string CustomerStatus { get; set; }
    public IEnumerable<DeleteAccountResponse> Accounts { get; set; }
}

public class DeleteAccountResponse
{
    public string AccountCode { get; set; }
    public string AccountStatus { get; set; }
}