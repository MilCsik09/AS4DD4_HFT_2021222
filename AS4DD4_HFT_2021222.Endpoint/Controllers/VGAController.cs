using AS4DD4_HFT_2021222.Logic;
using AS4DD4_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public VGAController(IComputerRepairLogic<VGA> cl)
        {
            this.cl = cl;
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
            cl.Delete(id);
        }

        [HttpPost]
        public void Post([FromBody] VGA vga)
        {
            cl.Create(vga);
        }

        [HttpPut]
        public void Update([FromBody] VGA vga)
        {
            cl.Update(vga);
        }
    }
}
