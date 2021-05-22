using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatanAPI.Data.DTO.Messages
{
    public class PrivateMessageFormDto
    {
        public string ToUserName { get; set; }
        public string Message { get; set; }
    }
}
