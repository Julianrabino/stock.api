﻿namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }
        
        public int ProductTypeId { get; set; }

        public string ProductTypeDesc { get; set; }
    }
}
