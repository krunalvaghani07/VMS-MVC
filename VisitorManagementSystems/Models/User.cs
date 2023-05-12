using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorManagementSystems.Models
{
    public class User
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Role_Id")]
        public int Role_Id { get; set; }
        [Column("UserGateId")]
        public int UserGateId { get; set; }
        [Column("UserRoleId")]
        public int UserRoleId { get; set; }
        [Column("GateId")]
        public int GateId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Role_Name")]
        public string Role_Name { get; set; }
        [Column("USER_NAME")]
        public string USER_NAME { get; set; }
        [Column("PASSWORD")]
        public string PASSWORD { get; set; }
        [Column("isActive")]
        public bool isActive { get; set; }
    }
}
