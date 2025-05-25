using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record ProductDTO (int ProductId, string ProductName, double Price,  string Description, string ImagePath);
}
