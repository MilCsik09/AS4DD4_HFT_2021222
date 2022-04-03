using AS4DD4_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Repository
{
    public partial class ComputerRepairDbContext : DbContext
    {
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<CPU> CPUs { get; set; }
        public virtual DbSet<VGA> VGAs { get; set; }


        public ComputerRepairDbContext()
        {
            this.Database.EnsureCreated();
        }
        public ComputerRepairDbContext(DbContextOptions<ComputerRepairDbContext> options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("computerrepairdb")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasOne(computer => computer.Cpu)
                    .WithMany(cpu => cpu.Computers)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasOne(computer => computer.Vga)
                    .WithMany(vga => vga.Computers)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<CPU>(entity =>
            {
                entity.HasOne(cpu => cpu.Brand)
                    .WithMany(brand => brand.Products)
                    .HasForeignKey(cpu => cpu.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<VGA>(entity =>
            {
                entity.HasOne(vga => vga.Brand)
                    .WithMany(brand => brand.Products)
                    .HasForeignKey(vga => vga.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            Brand<CPU> intel = new Brand<CPU>() { Id = 1, Name = "Intel" };
            Brand<CPU> amd = new Brand<CPU>() { Id = 2, Name = "AMD" };

            Brand<VGA> nvidia = new Brand<VGA>() { Id = 1, Name = "Nvidia" };
            Brand<VGA> amdvga = new Brand<VGA>() { Id = 2, Name = "AMD" };

            CPU icpu1 = new CPU() { Brand = intel, Model = "I7", Price = 1500};
            CPU icpu2 = new CPU() { Brand = intel, Model = "I9", Price = 3000 };

            CPU acpu1 = new CPU() { Brand = amd, Model = "Ryzen 7", Price = 1000 };
            CPU acpu2 = new CPU() { Brand = amd, Model = "Ryzen 9", Price = 2500 };

            VGA nvga1 = new VGA() { Brand = nvidia, Model = "GTX 1080TI", Price = 10000 };
            VGA nvga2 = new VGA() { Brand = nvidia, Model = "RTX 3080TI", Price = 50000 };

            VGA avga1 = new VGA() { Brand = amdvga, Model = "RX 6700", Price = 10000 };
            VGA avga2 = new VGA() { Brand = amdvga, Model = "RX 6800", Price = 30000 };

            Computer c1 = new Computer() { Cpu = icpu1, Vga = nvga1};
            Computer c2 = new Computer() { Cpu = icpu2, Vga = nvga2 };
            Computer c3 = new Computer() { Cpu = acpu1, Vga = avga1 };
            Computer c4 = new Computer() { Cpu = acpu2, Vga = avga2 };

            modelBuilder.Entity<Brand<CPU>>().HasData(intel, amd);
            modelBuilder.Entity<Brand<VGA>>().HasData(nvidia, amdvga);
            modelBuilder.Entity<CPU>().HasData(acpu1, acpu2, icpu1, icpu2);
            modelBuilder.Entity<VGA>().HasData(nvga1,nvga2, avga1, avga2);
            modelBuilder.Entity<Computer>().HasData(c1, c2, c3, c4);
        }
    }
}
