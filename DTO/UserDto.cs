using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BonsaiBackend.DTO {
    public class UserDto {
       public required string Name {get; set;} 
       public required string Email {get; set;}
       public required string Password {get; set;}
    }
}