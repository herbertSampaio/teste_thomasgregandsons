namespace Domain.DTOs
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Logotipo { get; set; }

        public List<AddressResponseDto> Logradouros { get; set; }
    }
}
