using AS4DD4_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public class CpuRepository : IComputerRepairRepository<CPU>
    {
        ComputerRepairDbContext context;

        public CpuRepository(ComputerRepairDbContext context)
        {
            this.context = context;
        }

        public void Create(CPU t)
        {
            context.CPUs.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.CPUs.Remove(ReadOne(id));
            context.SaveChanges();
        }

        public IQueryable<CPU> ReadAll()
        {
            return context.CPUs.AsQueryable();
        }

        public CPU ReadOne(int id)
        {
            return context.CPUs.FirstOrDefault(c => c.Id == id);
        }

        public void Update(CPU t)
        {
            CPU old = ReadOne(t.Id);
            old.Price = t.Price;
            old.Brand = t.Brand;
            old.Model = t.Model;
            old.Computers = t.Computers;
            
            context.SaveChanges();
        }
    }
}
