using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Api.Controllers
{
    [Route("api/producttype")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService service;
        private readonly IMapper mapper;
        
        public ProductTypeController(ProductTypeService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET /producttype
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList();
        }

        // GET /producttype/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(int id)
        {
            return this.mapper.Map<ProductTypeDTO>(this.service.Get(id));
        }

        // POST
        [HttpPost]
        public void Post([FromBody] ProductTypeDTO value)
        {
            this.service.Create(this.mapper.Map<ProductType>(value));
        }

        // PUT  /producttype/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductTypeDTO value)
        {
            var productType = this.service.Get(id);
            this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            this.service.Update(productType);
        }

        // DELETE /producttype/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var productType = this.service.Get(id);
            this.service.Delete(productType);
        }
    }
}
