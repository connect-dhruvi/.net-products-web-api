using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int USerID { get; set; }
        [JsonIgnore]

        public virtual User User { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}