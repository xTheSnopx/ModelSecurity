using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Pqr
    {
        public int PqrId { get; set; }
        public string PqrType { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string PqrStatus { get; set; }
        public DateTime ResolutionDate { get; set; }
        public int WorkerId { get; set; }
        public int ClientId { get; set; }

        public Worker Worker { get; set; }
        public Client Client { get; set; }
    }

}
