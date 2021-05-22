using CatanAPI.Models;
namespace CatanAPI.Data.DTO.UsersDTO
{
    public class GetUserSessionDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public GameSessionRoles Roles { get; set; }

    }
}