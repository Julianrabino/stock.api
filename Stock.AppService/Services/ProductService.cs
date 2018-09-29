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
    }
}
