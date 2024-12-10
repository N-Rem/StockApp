using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "The phone number is not valid.")]
        public string Phone { get; set; }

        public int? OwnerId { get; set; }

        [Required]
        public UserType Type { get; set; }
    

        public static UserDTO Create (User u)
        {
            var dto = new UserDTO();
            dto.Id = u.Id; 
            dto.Name = u.Name;
            dto.Email = u.Email;
            dto.Phone = u.Phone;
            dto.OwnerId = u.OwnerId;
            dto.Type = u.Type;
            return dto;
        }

        public static List<UserDTO?> CreateList(IEnumerable<User> users)
        {
            List<UserDTO?> listDto = [];

            foreach (var u in users)
            {
                listDto.Add(Create(u));
            }

            return listDto;
        }

    }
}
