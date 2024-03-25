namespace BackEndApi.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string? FechaDeNacimiento { get; set; }

        public string Cuit { get; set; } = null!;

        public string? Domicilio { get; set; }

        public string TelefonoCelular { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
