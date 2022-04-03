using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Models
{
    public class Brand<T> : Entity
    {
        public string Name { get; set; }  
        public virtual ICollection<T> Products { get; set; }
        public Brand()
        {
            this.Products = new HashSet<T>();
        }
    }
}
