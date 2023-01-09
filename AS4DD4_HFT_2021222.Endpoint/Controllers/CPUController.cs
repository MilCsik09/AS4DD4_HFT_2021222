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
    public class CPUController : ControllerBase
    {
        IComputerRepairLogic<CPU> cl;

        public CPUController(IComputerRepairLogic<CPU> cl)
        {
            this.cl = cl;
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
            cl.Delete(id);
        }

        [HttpPost]
        public void Post([FromBody] CPU cpu)
        {
            cl.Create(cpu);
        }

        [HttpPut]
        public void Update([FromBody] CPU cpu)
        {
            cl.Update(cpu);
        }
    }
}
