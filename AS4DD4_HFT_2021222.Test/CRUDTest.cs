using System;
using System.Collections.Generic;
using System.Linq;
using AS4DD4_HFT_2021222.Logic;
using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;

namespace AS4DD4_HFT_2021222.Test
{
    [TestFixture]
    class CRUDTests
    {
        private Mock<IComputerRepairRepository<CPU>> cpuMockRepo;
        private Mock<IComputerRepairRepository<VGA>> vgaMockRepo;
        private Mock<IComputerRepairRepository<Brand>> brandMockRepo;
        private Mock<IComputerRepairRepository<Computer>> computerMockRepo;

        private VgaLogic vgaController;
        private BrandLogic brandController;
        private CpuLogic cpuController;
        private ComputerLogic computerController;



        [SetUp]
        public void Init()
        {
            Brand intel = new Brand() { Id = 1, Name = "Intel" };
            Brand amd = new Brand() { Id = 2, Name = "AMD" };
            Brand nvidia = new Brand() { Id = 3, Name = "Nvidia" };
            Brand amdvga = new Brand() { Id = 4, Name = "AMD VGA" };

            CPU icpu1 = new CPU() { Id = 1, BrandId = intel.Id, Model = "I7", Price = 1500 };
            CPU icpu2 = new CPU() { Id = 2, BrandId = intel.Id, Model = "I9", Price = 3000 };
            CPU acpu1 = new CPU() { Id = 3, Model = "Ryzen 7", Price = 1000, BrandId = amd.Id };
            CPU acpu2 = new CPU() { Id = 4, Model = "Ryzen 9", Price = 2500, BrandId = amd.Id };

            VGA nvga1 = new VGA() { Id = 1, Model = "GTX 1080TI", Price = 10000, BrandId = nvidia.Id };
            VGA nvga2 = new VGA() { Id = 2, Model = "RTX 3080TI", Price = 50000, BrandId = nvidia.Id };
            VGA avga1 = new VGA() { Id = 3, Model = "RX 6700", Price = 10000, BrandId = amdvga.Id };
            VGA avga2 = new VGA() { Id = 4, Model = "RX 6800", Price = 30000, BrandId = amdvga.Id };

            Computer c1 = new Computer() { Id = 1, CpuId = icpu1.Id, VgaId = nvga1.Id };
            Computer c2 = new Computer() { Id = 2, CpuId = icpu2.Id, VgaId = nvga2.Id };
            Computer c3 = new Computer() { Id = 3, CpuId = acpu1.Id, VgaId = avga1.Id };
            Computer c4 = new Computer() { Id = 4, CpuId = acpu2.Id, VgaId = avga2.Id };


            this.cpuMockRepo = new Mock<IComputerRepairRepository<CPU>>();
            this.brandMockRepo = new Mock<IComputerRepairRepository<Brand>>();
            this.vgaMockRepo = new Mock<IComputerRepairRepository<VGA>>();
            this.computerMockRepo = new Mock<IComputerRepairRepository<Computer>>();

            this.cpuMockRepo.Setup(x => x.ReadAll()).Returns(new List<CPU>()
            {
                icpu1,icpu2,acpu1,acpu2
            }.AsQueryable);

            this.cpuController = new CpuLogic(this.cpuMockRepo.Object);

            this.brandMockRepo.Setup(x => x.ReadAll()).Returns(new List<Brand>()
            {
               intel,amd,amdvga,nvidia
            }.AsQueryable);

            this.brandController = new BrandLogic(this.brandMockRepo.Object);

            this.vgaMockRepo.Setup(x => x.ReadAll()).Returns(new List<VGA>()
            {
            nvga1,nvga2,avga1,avga2    
            }.AsQueryable);

            this.vgaController = new VgaLogic(this.vgaMockRepo.Object);

            this.computerMockRepo.Setup(x => x.ReadAll()).Returns(new List<Computer>()
            {
                c1,c2,c3,c4
            }.AsQueryable);

            this.computerController = new ComputerLogic(this.computerMockRepo.Object);
        }

        [Test]
        public void CreateNewBrandTest()
        {
            Brand brand = new Brand()
            {
                Name = "Teszt Brand"
                
            };
            this.brandController.Create(brand);

            this.brandMockRepo.Verify(x => x.Create(It.IsAny<Brand>()), Times.Once());
        }

        [Test]
        public void DeleteBrandTest()
        {

            this.brandController.Delete(1);

            this.brandMockRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void UpdateCPUTest()
        {
            this.cpuController.Update(new CPU() { Id = 1, BrandId = 1, Model = "I7", Price = 4500 });

            this.cpuMockRepo.Verify(x => x.Update(It.IsAny<CPU>()), Times.Once());
        }

        [Test]
        public void ReadAllComputerTest()
        {
            Assert.That(this.computerController.ReadAll().ToList().Count.Equals(4));
            this.computerMockRepo.Verify(x => x.ReadAll(), Times.Once);
        }

        [Test]
        public void ReadOneComputerTest()
        {
            Computer computer = this.computerController.ReadOne(1);
            this.computerMockRepo.Verify(x => x.ReadOne(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CreateNewCPUTest_WithExistingID()
        {
            CPU renter = new CPU() { Id = 1, BrandId = 1, Model = "TESZT CPU", Price = 15000000 };

            Assert.That(() => cpuController.Create(renter), Throws.Nothing);
        }


    }
}