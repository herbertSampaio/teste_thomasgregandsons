using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ClienteDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logotipo { get; set; }

        public List<AddressDto> Logradouros { get; set; }
    }
}
