using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorAccess_1.Models
{
    public class AccessRecord
    {
        public int RecordID { get; set; }
        public string PersonID { get; set; }
        public string PersonName { get; set; }
        public DateTime AccesTimeDate { get; set; }
        public int? DoorId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string DoorName { get; set; } // listede göstermek için (JOIN sonucu)
    }
}
