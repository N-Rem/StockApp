using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class SupplierProductUpdateRequestDTO
    {
        public int SupplierId { get; set; }

        public int ProductId { get; set; }
    }
}
