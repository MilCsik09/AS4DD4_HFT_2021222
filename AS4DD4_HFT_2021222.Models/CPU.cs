using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Models
{
    public class CPU : Entity
    {
        public string Model { get; set; }  
        public int? Price { get; set; }
        public virtual Brand<CPU> Brand { get; set; }  
        public virtual ICollection<Computer> Computers { get; set; }
        public int BrandId { get; set; }

    }
}
