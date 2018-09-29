using Stock.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Model.Entities
{
    [Table("product")]
    public class Product: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        //[ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
    }
}
