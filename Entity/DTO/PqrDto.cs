using System;

namespace DTO.Model
{
    public class PqrDto
    {
        public int PqrId { get; set; }
        public string PqrType { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string PqrStatus { get; set; }
        public DateTime ResolutionDate { get; set; }
        public int WorkerId { get; set; }
        public int ClientId { get; set; }
    }

}
