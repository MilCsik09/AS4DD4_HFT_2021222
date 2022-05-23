using AS4DD4_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public class VgaRepository : IComputerRepairRepository<VGA>
    {
        ComputerRepairDbContext context;

        public VgaRepository(ComputerRepairDbContext context)
        {
            this.context = context;
        }

        public void Create(VGA t)
        {
            context.VGAs.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.VGAs.Remove(ReadOne(id));
            context.SaveChanges();
        }

        public IQueryable<VGA> ReadAll()
        {
            return context.VGAs.AsQueryable();
        }

        public VGA ReadOne(int id)
        {
            return context.VGAs.FirstOrDefault(c => c.Id == id);
        }

        public void Update(VGA t)
        {
            VGA old = ReadOne(t.Id);
            old.Price = t.Price;
            old.Brand = t.Brand;
            old.Model = t.Model;

            context.SaveChanges();
        }
    }
}
