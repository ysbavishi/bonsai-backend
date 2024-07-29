using Microsoft.EntityFrameworkCore;

namespace BonsaiBackend.DTO
{
   public class TaskDTO
   {
        public required int UserId {get; set;}
        public required int ClientId {get; set;}
        public required string Description {get; set;}
        public string? newDescription {get; set;}
        public required bool Status {get; set;}
         
   } 
}