using System.Text.Json.Serialization;

namespace OnlineStore.Models
{
    public class ProductType
    {
        public ProductType(string type, int ageLmit ) {
            TypeName = type;
            AgeLimit = ageLmit;
        }
        public ProductType() { }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public int AgeLimit { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();

    }
}

