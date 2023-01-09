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
    public class BrandController : ControllerBase
    {
        IComputerRepairLogic<Brand> cl;

        public BrandController(IComputerRepairLogic<Brand> cl)
        {
            this.cl = cl;
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
            cl.Delete(id);
        }

        [HttpPost]
        public void Post([FromBody] Brand brand)
        {
            cl.Create(brand);
        }

        [HttpPut]
        public void Update([FromBody] Brand brand)
        {
            cl.Update(brand);
        }
    }
}
