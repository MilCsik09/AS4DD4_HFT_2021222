using AS4DD4_HFT_2021222.Models;
using ConsoleTools;
using System;

namespace AS4DD4_HFT_2021222.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {



            RestService rest = new RestService("http://localhost:21845/");

            var result1 = rest.Get<CPU>("api/cpu");
            var result2 = rest.Get<VGA>("api/vga");
            var result3 = rest.Get<Computer>("api/computer");


            var BrandMenu = new ConsoleMenu(args, level: 1)
        .Add("Create", () => rest.Post(new Brand() { Name="Teszt"}, "api/brand"))
        .Add("ReadOne", () => rest.GetSingle<Brand>("api/brand/1"))
        .Add("ReadAll", () => rest.Get<Brand>("api/brand"))
        .Add("Delete", () => { try { rest.Delete(3, "api/brand"); } catch (Exception e) { Console.WriteLine(e.Message); } })
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.Title = "BrandMenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var CpuMenu = new ConsoleMenu(args, level: 1)
        .Add("Create", () => rest.Post(new CPU() { BrandId = 1, isOperational = true, isUsed = false, Model = "TESZT CPU", Price = 10000 }, "api/cpu"))
        .Add("ReadOne", () => rest.GetSingle<CPU>("api/cpu/1"))
        .Add("ReadAll", () => rest.Get<CPU>("api/cpu"))
        .Add("Update", () => rest.Put(new CPU() { Id = 3, BrandId = 1, isOperational = true, isUsed = false, Model = "TESZT CPU", Price = 10000}, "api/cpu"))
        .Add("Delete", () => { try { rest.Delete(3, "api/cpu"); } catch (Exception e) { Console.WriteLine(e.Message); } })
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.Title = "CpuMenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var VgaMenu = new ConsoleMenu(args, level: 1)
        .Add("Create", () => rest.Post(new VGA() { BrandId = 3, isOperational = true, isUsed = false, Model = "TESZT VGA", Price = 90000 }, "api/vga"))
        .Add("ReadOne", () => rest.GetSingle<VGA>("api/vga/1"))
        .Add("ReadAll", () => rest.Get<VGA>("api/vga"))
        .Add("Update", () => rest.Put(new VGA() {Id = 3 ,BrandId = 3, isOperational = true, isUsed = false, Model = "TESZT VGA", Price = 90000 }, "api/vga"))
        .Add("Delete", () => { try { rest.Delete(3, "api/vga"); } catch (Exception e) { Console.WriteLine(e.Message); } })
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.Title = "VgaMenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var ComputerMenu = new ConsoleMenu(args, level: 1)
        .Add("Create", () => rest.Post(new Computer() { VgaId=1, CpuId = 3}, "api/computer"))
        .Add("ReadOne", () => rest.GetSingle<Computer>("api/computer/1"))
        .Add("ReadAll", () => rest.Get<Computer>("api/computer"))
        .Add("Delete", () => { try { rest.Delete(3, "api/computer"); } catch (Exception e) { Console.WriteLine(e.Message); } })
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.Title = "ComputerMenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var NonCrudMenu = new ConsoleMenu(args, level: 1)
        .Add("Computer/FilterOperational", () => rest.Get<Computer>("api/computer/FilterOperational"))
        .Add("Computer/FilterNonOperational", () => rest.Get<Computer>("api/computer/FilterNonOperational"))
        .Add("Computer/GetOverallRepairCost", () => { try { rest.Get<double>("api/computer/GetOverallRepairCost"); } catch (Exception e) { Console.WriteLine(e.Message); } })
        .Add("Computer/FilterPriceBetween", () => rest.Get<Computer>("api/computer/FilterPriceBetween/1000/20000"))
        .Add("Computer/FilterBrand", () => rest.Get<Computer>("api/computer/FilterBrand/intel"))
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.Title = "NonCrudMenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

            var menu = new ConsoleMenu(args, level: 0)
              .Add("CPU", CpuMenu.Show)
              .Add("VGA", VgaMenu.Show)
              .Add("Brand", BrandMenu.Show)
              .Add("Computer", ComputerMenu.Show)
              .Add("Non Crud", NonCrudMenu.Show)
              .Add("Close", ConsoleMenu.Close)
              .Add("Exit", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.Title = "Main menu";
                  config.EnableWriteTitle = true;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();




            ;
        }
    }
}
