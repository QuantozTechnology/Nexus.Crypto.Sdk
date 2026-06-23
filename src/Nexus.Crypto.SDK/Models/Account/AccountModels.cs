namespace Nexus.Crypto.SDK.Models.Account;

public class GetAccountResponse
{
    /// <summary>
    /// GUID to use in case AccountCode is missing.
    /// </summary>
    public string? Guid { get; set; }

    /// <summary>
    /// Unique identifier of the Account.
    /// </summary>
    public string? AccountCode { get; set; }

    /// <summary>
    /// Customer unique identifier.
    /// </summary>
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Timestamp of creation of the Account
    /// </summary>
    public string? Created { get; set; }

    /// <summary>
    /// Timestamp of activation of the Account
    /// </summary>
    public string? Activated { get; set; }

    /// <summary>
    /// Address in Nexus to receive cryptocurrency.
    /// </summary>
    public string? DcReceiveAddress { get; set; }

    /// <summary>
    /// Address of the Customer to send cryptocurrency, or user supplied address to send and receive Tokens.
    /// </summary>
    public string? CustomerCryptoAddress { get; set; }

    /// <summary>
    /// Cryptocurrency identifier.
    /// </summary>
    public string? DcCode { get; set; }

    /// <summary>
    /// Status of the account: New | Invalid | Valid | Active | Deleted
    /// </summary>
    public string? AccountStatus { get; set; }

    /// <summary>
    /// Type of the account: Custodian | Broker | Token
    /// </summary>
    public string? AccountType { get; set; }

    /// <summary>
    /// The bucket this account is connected to.
    /// </summary>
    public string? BucketCode { get; set; }

    /// <summary>
    /// Wallet service provider information.
    /// </summary>
    public AccountProvider? Provider { get; set; }
}

public class AccountProvider
{
    /// <summary>
    /// Provider type: Undefined | CustodianSeparateAddress | CustodianOmnibus | SelfHosted
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Name of the custodian or wallet app provider.
    /// </summary>
    public string? Name { get; set; }
}

public class CreateAccountRequest
{
    /// <summary>
    /// Type of the new account: Broker | BrokerBuyOnly | Custodian
    /// </summary>
    public string? AccountType { get; set; }

    /// <summary>
    /// Crypto address of the new account.
    /// </summary>
    public string? CustomerCryptoAddress { get; set; }

    /// <summary>
    /// Optional code in case the cryptocurrency cannot be automatically determined.
    /// </summary>
    public string? DcCode { get; set; }

    /// <summary>
    /// Cryptocurrency code of the account.
    /// </summary>
    public string? CryptoCode { get; set; }

    /// <summary>
    /// Optional account code used only for the creation of non-native accounts.
    /// </summary>
    public string? NativeAccountCode { get; set; }

    /// <summary>
    /// IP of the customer (deprecated; use customer_ip_address header instead).
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// Connect this account to a bucket.
    /// </summary>
    public string? BucketCode { get; set; }

    /// <summary>
    /// Settings required for creating a custodian based account.
    /// </summary>
    public CustodianSettingsRequest? CustodianSettings { get; set; }

    /// <summary>
    /// Wallet service provider information.
    /// </summary>
    public AccountProvider? Provider { get; set; }
}

public class CustodianSettingsRequest
{
    /// <summary>
    /// If true, Nexus will generate a receive address for this account.
    /// </summary>
    public bool GenerateReceiveAddress { get; set; }

    public bool GenerateUniqueReceiveAddress { get; set; }
}

public class CreateAccountResponse
{
    public string? Guid { get; set; }
    public string? AccountCode { get; set; }
    public string? CustomerCode { get; set; }
    public string? Created { get; set; }
    public string? Activated { get; set; }
    public string? DcReceiveAddress { get; set; }
    public string? CustomerCryptoAddress { get; set; }
    public string? DcCode { get; set; }
    public string? AccountStatus { get; set; }
    public string? AccountType { get; set; }
    public string? BucketCode { get; set; }
    public AccountProvider? Provider { get; set; }
}

public class UpdateAccountRequest
{
    /// <summary>
    /// Wallet service provider information.
    /// </summary>
    public AccountProvider? Provider { get; set; }
}

public class CreateNonNativeAccountRequest
{
    /// <summary>
    /// This account will be used as native account.
    /// </summary>
    public string? AccountCode { get; set; }

    /// <summary>
    /// Crypto address of the new account.
    /// </summary>
    public string? CustomerCryptoAddress { get; set; }

    /// <summary>
    /// Cryptocurrency code of the account.
    /// </summary>
    public string? CryptoCode { get; set; }

    /// <summary>
    /// Connect this account to a bucket.
    /// </summary>
    public string? BucketCode { get; set; }

    /// <summary>
    /// Wallet service provider information.
    /// </summary>
    public AccountProvider? Provider { get; set; }
}
