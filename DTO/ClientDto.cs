using BonsaiBackend.Models;
using Microsoft.Identity.Client;

namespace BonsaiBackend.DTO {
    public class ClientDTO {
        public required string Name {get; set;}
        public required string Email {get; set;}
        public required int UserId {get; set;}
    }
}