using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonsaiBackend.Models {
    public class TaskTimes {
        [Key]
        public int Id {get; set;}
        [ForeignKey("Users")]
        public int UserId {get;set;}
        public virtual Users Users {get; set;}
        [ForeignKey("Clients")]
        public int ClientId {get;set;}
        public virtual Clients Clients {get; set;}
        [ForeignKey("Tasks")]
        public int? TaskId {get; set;}
        public virtual Tasks Tasks {get;set;}
        public DateTime? CurrentDate {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
    }
}