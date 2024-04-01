using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonsaiBackend.Models {
    public class Payments {
        [Key]
        public int Id {get; set;}
        [ForeignKey("Clients")]
        public int ClientId{get; set;}
        public virtual Clients Clients {get; set;}
        [ForeignKey("Users")]
        public int UserId{get; set;}
        public virtual Users Users {get; set;}
        [ForeignKey("Tasks")]
        public int TaskId{get; set;}
        public virtual Tasks Tasks {get; set;}
        [ForeignKey("TaskTimes")]
        public int TimeId{get; set;}
        public virtual TaskTimes TaskTimes {get; set;}
        [Column(TypeName = "decimal(10,2)")]
        public decimal outstanding{get; set;}

        [Column(TypeName = "decimal(10,2)")]
        public decimal overdue{get; set;}
        [Column(TypeName = "decimal(10,2)")]
        public decimal paid{get; set;}

    }
}