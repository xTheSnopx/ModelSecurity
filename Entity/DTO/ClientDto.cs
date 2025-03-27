using System;

namespace DTO.Model
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityDocument { get; set; }
        public string ClientType { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int SocioeconomicStratification { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}
