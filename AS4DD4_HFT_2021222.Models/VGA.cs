using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Models
{
    public class VGA : Entity
    {
        public string Model { get; set; }
        public int Price { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
