using CatanAPI.Models;
using System;


namespace CatanAPI.Data.DTO.GameSessionsDTO
{
    public class SetSessionStatusRequest
    {
        public GameSessionUserStatus Status { get; set; }
    }

}