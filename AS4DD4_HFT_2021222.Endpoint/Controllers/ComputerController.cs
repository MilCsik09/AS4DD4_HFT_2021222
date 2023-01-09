using AS4DD4_HFT_2021222.Endpoint.Services;
using AS4DD4_HFT_2021222.Logic;
using AS4DD4_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        IComputerRepairLogic<Computer> cl;
        IHubContext<SignalRHub> hub;

        public ComputerController(IComputerRepairLogic<Computer> cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Computer> ReadAll()
        {
            return cl.ReadAll();
        }

        [HttpGet("{id}")]
        public Computer ReadOne(int id)
        {
            return cl.ReadOne(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var ComputerToDelete = cl.ReadOne(id);
            cl.Delete(id);
            hub.Clients.All.SendAsync("ComputerDeleted", ComputerToDelete);
        }

        [HttpPost]
        public void Post([FromBody] Computer comp)
        {
            cl.Create(comp);
            hub.Clients.All.SendAsync("ComputerCreated", comp);
        }

        [HttpPut]
        public void Update([FromBody] Computer comp)
        {
            cl.Update(comp);
            hub.Clients.All.SendAsync("ComputerUpdated", comp);
        }

        [HttpGet("FilterPriceBetween/{min}/{max}")]
        public IEnumerable<Computer> FilterPriceBetween(int min, int max)
        {
            return (cl as ComputerLogic).FilterPriceBetween(min, max);
        }

        [HttpGet("FilterBrand/{brand}")]
        public IEnumerable<Computer> BrandFilter(string brand)
        {
            return (cl as ComputerLogic).FilterBrand(brand);
        }

        [HttpGet("FilterOperational")]
        public IEnumerable<Computer> FilterOperational()
        {
            return (cl as ComputerLogic).FilterOperational();
        }

        [HttpGet("FilterNonOperational")]
        public IEnumerable<Computer> FilterNonOperational()
        {
            return (cl as ComputerLogic).FilterNonOperational();
        }

        [HttpGet("GetOverallRepairCost")]
        public double GetOverallRepairCost()
        {
            return (cl as ComputerLogic).GetOverallRepairCost();
        }


    }
}
