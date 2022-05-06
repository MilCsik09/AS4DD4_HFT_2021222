using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Logic
{
    public class CpuLogic : IComputerRepairLogic<CPU>
    {
        IComputerRepairRepository<CPU> repo;

        public void Create(CPU t)
        {
            repo.Create(t);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public IQueryable<CPU> ReadAll()
        {
            return repo.ReadAll();
        }

        public CPU ReadOne(int id)
        {
            return repo.ReadOne(id);
        }

        public void Update(CPU t)
        {
            repo.Update(t);
        }
    }
}
