using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserVerificationModel
    {
        public int UserId { get; set; }

        [Required]
        public Guid UserUniqueId { get; set; }
    }
}
