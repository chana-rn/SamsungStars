using System.Collections.Generic;

namespace DTO
{
    public record CategoryDTO(int CategoryId, string CategoryName, List<ProductDTO> Products);
}
