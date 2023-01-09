using AS4DD4_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public class BrandRepository : IComputerRepairRepository<Brand>
    {
        ComputerRepairDbContext context;
        public BrandRepository(ComputerRepairDbContext context)
        {
            this.context = context;
        }

        public void Create(Brand t)
        {
            context.Brands.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Brands.Remove(ReadOne(id));
            context.SaveChanges();
        }

        public IQueryable<Brand> ReadAll()
        {
            return context.Brands.AsQueryable();
        }

        public Brand ReadOne(int id)
        {
            return context.Brands.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Brand t)
        {
            Brand old = ReadOne(t.Id);

            old.Name = t.Name;

            context.SaveChanges();
        }
    }
}
