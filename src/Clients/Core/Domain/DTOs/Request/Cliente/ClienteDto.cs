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
