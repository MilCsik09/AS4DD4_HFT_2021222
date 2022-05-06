using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Models
{
    public class Computer : Entity
    {
        [ForeignKey(nameof(CPU))]
        public int CpuId { get; set; }

        [ForeignKey(nameof(VGA))]
        public int VgaId { get; set; }
        public virtual CPU Cpu { get; set; }
        public virtual VGA Vga { get; set; }

        public int getPrice()
        {
            return (Cpu.Price) + (Vga.Price);
        }

        
    }
}
