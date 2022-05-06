using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Models
{
    public class Brand : Entity
    {
        public string Name { get; set; }  
        public virtual ICollection<CPU> CpuProducts { get; set; }
        public virtual ICollection<VGA> VgaProducts { get; set; }
        public Brand()
        {
            this.CpuProducts = new HashSet<CPU>();
            this.VgaProducts = new HashSet<VGA>();
        }
    }
}
