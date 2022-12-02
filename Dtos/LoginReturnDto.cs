using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_app_backend.Dtos
{
    public class LoginReturnDto
    {
        public string Jwt { get; set; }
        public string CreatedAt { get; set; }

    }
}