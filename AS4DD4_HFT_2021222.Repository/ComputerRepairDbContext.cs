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
        public virtual DbSet<Brand> Brands { get; set; }


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
            modelBuilder.Entity<CPU>(entity =>
            {
                entity.HasOne(cpu => cpu.Brand)
                    .WithMany(brand => brand.CpuProducts)
                    .HasForeignKey(cpu => cpu.BrandId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });
            modelBuilder.Entity<VGA>(entity =>
            {
                entity.HasOne(vga => vga.Brand)
                    .WithMany(brand => brand.VgaProducts)
                    .HasForeignKey(vga => vga.BrandId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasOne(computer => computer.Cpu)
                    .WithMany(cpu => cpu.Computers)
                    .HasForeignKey(computer => computer.CpuId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasOne(computer => computer.Vga)
                    .WithMany(vga => vga.Computers)
                    .HasForeignKey(computer => computer.VgaId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });


            Brand intel = new Brand() { Id = 1, Name = "Intel" };
            Brand amd = new Brand() { Id = 2, Name = "AMD" };
            Brand nvidia = new Brand() { Id = 3, Name = "Nvidia" };
            Brand amdvga = new Brand() { Id = 4, Name = "AMD VGA" };

            CPU icpu1 = new CPU() { Id = 1, BrandId = intel.Id, Model = "I7", Price = 1500, isUsed = true, isOperational = true };
            CPU icpu2 = new CPU() { Id = 2, BrandId = intel.Id, Model = "I9", Price = 3000, isUsed = true, isOperational = true };
            CPU acpu1 = new CPU() { Id = 3, Model = "Ryzen 7", Price = 1000, BrandId = amd.Id, isUsed = true, isOperational = false };
            CPU acpu2 = new CPU() { Id = 4, Model = "Ryzen 9", Price = 2500, BrandId = amd.Id, isUsed = true, isOperational = true };

            VGA nvga1 = new VGA() { Id = 1, Model = "GTX 1080TI", Price = 10000, BrandId = nvidia.Id, isUsed = true, isOperational = false };
            VGA nvga2 = new VGA() { Id = 2, Model = "RTX 3080TI", Price = 50000, BrandId = nvidia.Id, isUsed = true, isOperational = true };
            VGA avga1 = new VGA() { Id = 3, Model = "RX 6700", Price = 10000, BrandId = amdvga.Id, isUsed = true, isOperational = true };
            VGA avga2 = new VGA() { Id = 4, Model = "RX 6800", Price = 30000, BrandId = amdvga.Id, isUsed = true, isOperational = false };

            Computer c1 = new Computer() { Id = 1, CpuId = icpu1.Id, VgaId = nvga1.Id };
            Computer c2 = new Computer() { Id = 2, CpuId = icpu2.Id, VgaId = nvga2.Id };
            Computer c3 = new Computer() { Id = 3, CpuId = acpu1.Id, VgaId = avga1.Id };
            Computer c4 = new Computer() { Id = 4, CpuId = acpu2.Id, VgaId = avga2.Id };

            modelBuilder.Entity<Brand>().HasData(intel, amd, nvidia, amdvga);
            modelBuilder.Entity<CPU>().HasData(acpu1, acpu2, icpu1, icpu2);
            modelBuilder.Entity<VGA>().HasData(nvga1, nvga2, avga1, avga2);
            modelBuilder.Entity<Computer>().HasData(c1, c2, c3, c4);
        }
    }
}
