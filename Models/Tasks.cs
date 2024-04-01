using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonsaiBackend.Models
{
    public class Tasks {
        [Key]
        public int TaskId {get; set;}
        [ForeignKey("Client")]
        public int ClientId{get; set;}
        public virtual Clients Clients {get; set;}
        [ForeignKey("User")]
        public int UserId {get; set;}
        public virtual Users Users {get; set;}
        public bool Status {get; set;}
    }
}