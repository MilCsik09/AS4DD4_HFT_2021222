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
    public class VGAController : ControllerBase
    {
        IComputerRepairLogic<VGA> cl;
        IHubContext<SignalRHub> hub;

        public VGAController(IComputerRepairLogic<VGA> cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<VGA> ReadAll()
        {
            return cl.ReadAll();
        }

        [HttpGet("{id}")]
        public VGA ReadOne(int id)
        {
            return cl.ReadOne(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var VGAToDelete = cl.ReadOne(id);
            cl.Delete(id);
            hub.Clients.All.SendAsync("VGADeleted", VGAToDelete);
        }

        [HttpPost]
        public void Post([FromBody] VGA vga)
        {
            cl.Create(vga);
            hub.Clients.All.SendAsync("VGACreated", vga);
        }

        [HttpPut]
        public void Update([FromBody] VGA vga)
        {
            cl.Update(vga);
            hub.Clients.All.SendAsync("VGAUpdated", vga);
        }
    }
}
