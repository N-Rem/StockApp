using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class SupplierProductDTO
    {
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int ProductId { get; set; }



        public static SupplierProductDTO Create(SupplierProduct sp)
        {
            var dto = new SupplierProductDTO();
            dto.Id = sp.Id;
            dto.SupplierId = sp.SupplierId;
            dto.ProductId = sp.ProductId;

            return dto;
        }

        public static List<SupplierProductDTO?> CreateList(IEnumerable<SupplierProduct> supplierProducts)
        {
            List<SupplierProductDTO> listDto = [];

            foreach (var sp in supplierProducts)
            {
                listDto.Add(Create(sp));
            }

            return listDto;
        }

    }
}
