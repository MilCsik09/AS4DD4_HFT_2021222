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
    public class BrandController : ControllerBase
    {
        IComputerRepairLogic<Brand> cl;
        IHubContext<SignalRHub> hub;

        public BrandController(IComputerRepairLogic<Brand> cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Brand> ReadAll()
        {
            return cl.ReadAll();
        }

        [HttpGet("{id}")]
        public Brand ReadOne(int id)
        {
            return cl.ReadOne(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var BrandToDelete = cl.ReadOne(id);
            cl.Delete(id);
            hub.Clients.All.SendAsync("BrandDeleted", BrandToDelete);
        }

        [HttpPost]
        public void Post([FromBody] Brand brand)
        {
            cl.Create(brand);
            hub.Clients.All.SendAsync("BrandCreated", brand);
        }

        [HttpPut]
        public void Update([FromBody] Brand brand)
        {
            cl.Update(brand);
            hub.Clients.All.SendAsync("BrandUpdated", brand);
        }
    }
}
