using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.Repositories;
using System;

namespace Stock.AppService.Services
{
    public class ProductService: BaseService<Product>
    {                
        public ProductService(ProductRepository repository)
        {
            this.Repository = repository;
        }

        public int ObtenerStock(int idProducto)
        {
            var producto = this.Repository.Get(idProducto);
            return producto.Stock;
        }

        public void DescontarStock(int idProducto, int value)
        {
            var producto = this.Repository.Get(idProducto);
            producto.DescontarStock(value);
            this.Repository.Update(producto);
        }

        public void SumarStock(int idProducto, int value)
        {
            var producto = this.Repository.Get(idProducto);
            producto.SumarStock(value);
            this.Repository.Update(producto);
        }
    }
}
