using System;

namespace Nexus.LabelApi.SDK.Models
{
    public class GetToken
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TokenType { get; set; }
        public string AssetType { get; set; }
        public string OwnerType { get; set; }
        public string Status { get; set; }
        public string IssuerAddress { get; set; }
        public PeggedBy PeggedBy { get; set; }
        public DateTime? Created { get; set; }
    }

    public class PeggedBy
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }

    }
}
