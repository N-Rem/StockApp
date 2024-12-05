using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class SupplierProductCreateRequestDTO
    {
        public int SupplierId { get; set; }

        public int ProductId { get; set; }
    }
}
