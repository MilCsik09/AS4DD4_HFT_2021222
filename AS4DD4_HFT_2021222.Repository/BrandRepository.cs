using AS4DD4_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public class BrandRepository : IComputerRepairRepository<Brand<Type>>
    {
        ComputerRepairDbContext context;
        public BrandRepository(ComputerRepairDbContext context)
        {
            this.context = context;
        }

        public void Create(Brand<Type> t)
        {
            context.Brands.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Brands.Remove(ReadOne(id));
            context.SaveChanges();
        }

        public IQueryable<Brand<Type>> ReadAll()
        {
            return context.Brands.AsQueryable();
        }

        public Brand<Type> ReadOne(int id)
        {
            return context.Brands.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Brand<Type> t)
        {
            Brand<Type> old = ReadOne(t.Id);

            old.Products = t.Products;
            old.Name = t.Name;

            context.SaveChanges();
        }
    }
}
