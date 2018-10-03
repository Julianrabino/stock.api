using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
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

        /// <summary>
        /// Permite recuperar todas entidades
        /// </summary>
        /// <returns>Una colección de entidades</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar una entidad mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la entidad a recuperar</param>
        /// <returns>Una entidad</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        /// <summary>
        /// Permite crear una nueva entidad
        /// </summary>
        /// <param name="value">Una entidad</param>
        [HttpPost]
        public void Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);
            var product = this.mapper.Map<Product>(value);
            product.ProductType = this.productTypeService.Get(value.ProductTypeId.Value);
            this.service.Create(product);
        }

        /// <summary>
        /// Permite editar una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad a editar</param>
        /// <param name="value">Una entidad con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            product.ProductType = this.productTypeService.Get(value.ProductTypeId.Value);
            this.service.Update(product);
        }

        /// <summary>
        /// Permite borrar una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad a borrar</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = this.service.Get(id);
            this.service.Delete(product);
        }

        /// <summary>
        /// Permite conocer el stock de un producto
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>El stock disponible</returns>
        [HttpGet("stock/{id}")]
        public ActionResult<int> ObtenerStock(int id)
        {
            return this.service.ObtenerStock(id);
        }

        /// <summary>
        /// Permite descontar una cantidad de stock a un producto
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <param name="value">La cantidad a descontar</param>
        [HttpPut("stock/descontar/{id}")]
        public void DescontarStock(int id, [FromBody] int value)
        {
            this.service.DescontarStock(id, value);
        }

        /// <summary>
        /// Permite sumar una cantidad de stock a un productor
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <param name="value">La cantidad a sumar</param>
        [HttpPut("stock/sumar/{id}")]
        public void SumarStock(int id, [FromBody] int value)
        {
            this.service.SumarStock(id, value);
        }
    }
}
