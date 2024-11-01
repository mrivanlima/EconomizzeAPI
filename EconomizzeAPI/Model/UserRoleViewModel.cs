﻿using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class UserRoleViewModel
    {
        public short RoleId { get; set; }

		[Required(ErrorMessage = "usuario ID necessario")]
		public int UserId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? RoleStartDate { get; set; }

        public DateTime? RoleEndDate { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
