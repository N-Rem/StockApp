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
    public class HistoricalPriceDTO
    {
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime PriceDate {  get; set; }

        [Required]
        public int ProductId { get; set; }



        public static HistoricalPriceDTO Create(HistoricalPrice hp)
        {
            var dto = new HistoricalPriceDTO();
            dto.Id = hp.Id;
            dto.Price = hp.Price;
            dto.PriceDate = hp.PriceDate;
            dto.ProductId = hp.ProductId;

            return dto;
        }

        public static List<HistoricalPriceDTO> CreateList(IEnumerable<HistoricalPrice> hps)
        {
            List<HistoricalPriceDTO> listDto = [];

            foreach (var hp in hps)
            {
                listDto.Add(Create(hp));
            }

            return listDto;
        }
    }
}
