using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class GenericResultDTO<T>
    {
        public GenericResultDTO(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}
