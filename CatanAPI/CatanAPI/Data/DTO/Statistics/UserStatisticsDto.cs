using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatanAPI.Data.DTO
{
    public class UserStatisticsDto
    {
        public int NoOfGames { get; set; }
        public int NoOfWonGames { get; set; }
        public int TimeOnPlay { get; set; }
    }
}
