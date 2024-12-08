﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class BranchCreateRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Addres { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Tel { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}
