using System;


namespace CatanAPI.Data.DTO.GameSessionsDTO
{
    public class GetSessionMinDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}