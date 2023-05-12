using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorManagementSystems.Models
{
    public class Visitor
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Purpose")]
        public string Purpose { get; set; }
        [Column("Entry_Time")]
        public DateTime Entry_Time { get; set; }
        [Column("Exit_Time")]
        public DateTime ? Exit_Time { get; set; }
        [Column("Photo")]
        public string Photo { get; set; }
        [Column("Person_to_Meet")]
        public string Person_to_Meet { get; set; }
        [Column("Department")]
        public string Department { get; set; }
        [Column("Carried_Assets")]
        public string Carried_Assets { get; set; }
        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [Column("ModifiedOn")]
        public DateTime ModifiedOn { get; set; }
        [Column("CreatedBy")]
        public int CreatedBy { get; set; }
        [Column("ModifiedBy")]
        public int ModifiedBy { get; set; }

    }
}
