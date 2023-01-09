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
    public class CPUController : ControllerBase
    {
        IComputerRepairLogic<CPU> cl;
        IHubContext<SignalRHub> hub;

        public CPUController(IComputerRepairLogic<CPU> cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<CPU> ReadAll()
        {
            return cl.ReadAll();
        }

        [HttpGet("{id}")]
        public CPU ReadOne(int id)
        {
            return cl.ReadOne(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var CPUToDelete = cl.ReadOne(id);
            cl.Delete(id);
            hub.Clients.All.SendAsync("CPUDeleted", CPUToDelete);
        }

        [HttpPost]
        public void Post([FromBody] CPU cpu)
        {
            cl.Create(cpu);
            hub.Clients.All.SendAsync("CPUCreated", cpu);
        }

        [HttpPut]
        public void Update([FromBody] CPU cpu)
        {
            cl.Update(cpu);
            hub.Clients.All.SendAsync("CPUUpdated", cpu);
        }
    }
}
