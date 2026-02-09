namespace Nexus.Crypto.SDK.Models;

public class GetCustomerBankAccounts
{
    public Guid Id { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string CustomerCode { get; set; }

    public string CurrencyCode { get; set; }

    public BankResponse Bank { get; set; }

    public bool IsPrimary { get; set; }

    public string Created { get; set; }

    public string CreatedBy { get; set; }

    public string Updated { get; set; }

    public string UpdatedBy { get; set; }
}

public class BankResponse
    {
        private string _guid;
        private string _name;
        private string _country;
        private string _bicCode;
        private string _city;
        
        public string Id
        {
            get
            {
                return _guid?.Trim();
            }
            set
            {
                _guid = value;
            }
        }
        
        public string Name
        {
            get
            {
                return _name?.Trim();
            }
            set
            {
                _name = value;
            }
        }

        public string Country
        {
            get
            {
                return _country?.Trim();
            }
            set
            {
                _country = value;
            }
        }
        
        public string BicCode
        {
            get
            {
                return _bicCode?.Trim();
            }
            set
            {
                _bicCode = value;
            }
        }
        
        public string City
        {
            get
            {
                return _city?.Trim();
            }
            set
            {
                _city = value;
            }
        }
    }