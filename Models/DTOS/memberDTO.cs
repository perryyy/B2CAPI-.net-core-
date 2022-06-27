using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.Models.DTOS
{
    public class memberDTO
    {
        public int Mid { get; set; }
        public string Username { get; set; }
        public string Sex { get; set; }
        public DateTime? Birth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string auth { get; set; }
        public string token { get; set; }
    }
}
