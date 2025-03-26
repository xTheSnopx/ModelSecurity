namespace DTO.Model
{
    public class CreateUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // ⚠️ Solo se usa en la creación
    }
}
