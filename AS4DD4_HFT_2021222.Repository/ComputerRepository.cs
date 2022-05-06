using AS4DD4_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public class ComputerRepository : IComputerRepairRepository<Computer>
    {
        ComputerRepairDbContext context;

        public ComputerRepository(ComputerRepairDbContext context)
        {
            this.context = context;
        }
        public void Create(Computer t)
        {
            context.Computers.Add(t);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Computers.Remove(ReadOne(id));
            context.SaveChanges();
        }

        public IQueryable<Computer> ReadAll()
        {
            return context.Computers.AsQueryable();
        }

        public Computer ReadOne(int id)
        {
            return context.Computers.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Computer t)
        {
            Computer old = ReadOne(t.Id);

            old.Cpu = t.Cpu;
            old.Vga = t.Vga;

            context.SaveChanges();
        }
    }
}
