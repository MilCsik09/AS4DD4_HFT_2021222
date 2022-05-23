using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Logic
{
    public class ComputerLogic : IComputerRepairLogic<Computer>
    {
        IComputerRepairRepository<Computer> repo;

        public ComputerLogic(IComputerRepairRepository<Computer> repo)
        {
            this.repo = repo;
        }

        public void Create(Computer t)
        {
            repo.Create(t);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public IQueryable<Computer> ReadAll()
        {
            return repo.ReadAll();
        }

        public Computer ReadOne(int id)
        {
            return repo.ReadOne(id);
        }

        public void Update(Computer t)
        {
             repo.Update(t);
        }
        
        public IEnumerable<Computer> FilterPriceBetween(int MinPrice, int MaxPrice)
        {
            List<Computer> filtered = new List<Computer>();
            foreach (var item in this.repo.ReadAll())
            {
                if ((item.Cpu.Price + item.Vga.Price) > MinPrice)
                {
                    if ((item.Cpu.Price + item.Vga.Price) < MaxPrice)
                    {
                        filtered.Add(item);
                    }
                }
            }

            return filtered;
        }

        public IEnumerable<Computer> FilterBrand(string brandName)
        {
            List<Computer> filtered = new List<Computer>();
            foreach (var item in this.repo.ReadAll())
            {
                if (item.Cpu.Brand.Name.ToLower().Equals(brandName.ToLower()) || item.Vga.Brand.Name.ToLower().Equals(brandName.ToLower()))
                {
                        filtered.Add(item);
                }
            }

            return filtered;
        }

        public IEnumerable<Computer> FilterOperational()
        {
            List<Computer> filtered = new List<Computer>();
            foreach (var item in this.repo.ReadAll())
            {
                if (item.Cpu.isOperational && item.Vga.isOperational)
                {
                    filtered.Add(item);
                }
            }

            return filtered;
        }
        public IEnumerable<Computer> FilterNonOperational()
        {
            List<Computer> filtered = new List<Computer>();
            foreach (var item in this.repo.ReadAll())
            {
                if (!item.Cpu.isOperational || !item.Vga.isOperational)
                {
                    filtered.Add(item);
                }
            }

            return filtered;
        }

        public double GetOverallRepairCost()
        {
            List<Computer> badComputers = FilterNonOperational().ToList();
            double totalRepairCost = 0;
            foreach (var item in badComputers)
            {
                if (!item.Cpu.isOperational)
                {
                    totalRepairCost += item.Cpu.Price;
                }
                if (!item.Vga.isOperational)
                {
                    totalRepairCost += item.Vga.Price;
                }
            }
            return totalRepairCost;
        }
    }
}
