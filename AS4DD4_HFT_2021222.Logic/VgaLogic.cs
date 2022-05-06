using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Logic
{
    public class VgaLogic : IComputerRepairLogic<VGA>
    {
        IComputerRepairRepository<VGA> repo;

        public void Create(VGA t)
        {
            repo.Create(t);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public IQueryable<VGA> ReadAll()
        {
            return repo.ReadAll();
        }

        public VGA ReadOne(int id)
        {
            return repo.ReadOne(id);
        }

        public void Update(VGA t)
        {
            repo.Update(t);
        }
    }
}
