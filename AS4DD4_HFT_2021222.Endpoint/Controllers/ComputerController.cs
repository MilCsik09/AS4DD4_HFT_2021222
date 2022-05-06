using AS4DD4_HFT_2021222.Logic;
using AS4DD4_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        IComputerRepairLogic<Computer> cl;

        public ComputerController(IComputerRepairLogic<Computer> cl)
        {
            this.cl = cl;
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
            cl.Delete(id);
        }

        [HttpPost]
        public void Post([FromBody] Computer comp)
        {
            cl.Create(comp);
        }

        [HttpPut]
        public void Update([FromBody] Computer comp)
        {
            cl.Update(comp);
        }
    }
}
