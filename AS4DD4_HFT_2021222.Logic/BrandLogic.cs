using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Logic
{
    public class BrandLogic : IComputerRepairLogic<Brand<Type>>
    {
        IComputerRepairRepository<Brand<Type>> repo;

        public void Create(Brand<Type> t)
        {
            repo.Create(t);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public IQueryable<Brand<Type>> ReadAll()
        {
            return repo.ReadAll();
        }

        public Brand<Type> ReadOne(int id)
        {
            return repo.ReadOne(id);
        }

        public void Update(Brand<Type> t)
        {
            repo.Update(t);
        }
    }
}
