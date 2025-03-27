using System;

namespace DTO.Model
{
    public class WorkerLoginDto
    {
        public int LoginId { get; set; }
        public int WorkerId { get; set; }
        public string Username { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }

}
