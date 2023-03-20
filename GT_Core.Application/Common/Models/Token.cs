using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_Core.Application.Common.Models
{
    public class JWToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
