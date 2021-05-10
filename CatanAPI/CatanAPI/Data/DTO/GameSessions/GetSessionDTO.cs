using CatanAPI.Models;
using CatanAPI.Data.DTO.UsersDTO;
using System;
using System.Collections.Generic;

namespace CatanAPI.Data.DTO.GameSessionsDTO
{
    public class GetSessionDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Extension> Extensions { get; set; }
        public List<GetUserMinDTO> GameSessionUsers { get; set; }
    }

}