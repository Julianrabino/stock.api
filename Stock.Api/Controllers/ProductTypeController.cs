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

        /// <summary>
        /// Permite recuperar todas entidades
        /// </summary>
        /// <returns>Una colección de entidades</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar una entidad mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la entidad a recuperar</param>
        /// <returns>Una entidad</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(int id)
        {
            return this.mapper.Map<ProductTypeDTO>(this.service.Get(id));
        }

        /// <summary>
        /// Permite crear una nueva entidad
        /// </summary>
        /// <param name="value">Una entidad</param>
        [HttpPost]
        public void Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);
            this.service.Create(this.mapper.Map<ProductType>(value));
        }

        /// <summary>
        /// Permite editar una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad a editar</param>
        /// <param name="value">Una entidad con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductTypeDTO value)
        {
            var productType = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            this.service.Update(productType);
        }

        /// <summary>
        /// Permite borrar una entidad
        /// </summary>
        /// <param name="id">Identificador de la entidad a borrar</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var productType = this.service.Get(id);
            this.service.Delete(productType);
        }
    }
}
