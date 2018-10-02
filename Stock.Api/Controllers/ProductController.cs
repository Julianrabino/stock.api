using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly ProductTypeService productTypeService;
        private readonly IMapper mapper;

        public ProductController(ProductService service, ProductTypeService productTypeService, IMapper mapper)
        {
            this.service = service;
            this.productTypeService = productTypeService;
            this.mapper = mapper;
        }

        // GET /product
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductDTO>>(this.service.GetAll()).ToList();
        }

        // GET /product/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        // POST
        [HttpPost]
        public void Post([FromBody] ProductDTO value)
        {
            var product = this.mapper.Map<Product>(value);
            product.ProductType = this.productTypeService.Get(value.ProductTypeId);
            this.service.Create(product);
        }

        // PUT  /product/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            this.mapper.Map<ProductDTO, Product>(value, product);
            product.ProductType = this.productTypeService.Get(value.ProductTypeId);
            this.service.Update(product);
        }

        // DELETE /product/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = this.service.Get(id);
            this.service.Delete(product);
        }

        // GET /product/stock/{id}
        [HttpGet("stock/{idProducto}")]
        public ActionResult<int> ObtenerStock(int idProducto)
        {
            return this.service.ObtenerStock(idProducto);
        }

        // GET /product/stock/descontar/{id}
        [HttpPut("stock/descontar/{idProducto}")]
        public void DescontarStock(int idProducto, [FromBody] int value)
        {
            this.service.DescontarStock(idProducto, value);
        }

        // GET /product/stock/sumar/{id}
        [HttpPut("stock/sumar/{idProducto}")]
        public void SumarStock(int idProducto, [FromBody] int value)
        {
            this.service.SumarStock(idProducto, value);
        }
    }
}
