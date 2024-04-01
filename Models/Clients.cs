using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonsaiBackend.Models
{
   public class Clients {
    [Key]
    public int Id {get; set;}
    [ForeignKey("Users")]
    public int UserId{get; set;}
    public virtual Users Users {get; set;}
    public required string Name{get;set;}
    public required string Email{get;set;}
   } 
}