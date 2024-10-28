using System.ComponentModel.DataAnnotations;

namespace EconomizzeAPI.Model
{
    public class DrugstoreNeighborhoodSubscriptionViewModel
    {
       
        public int DrugstoreId { get; set; }


        public int NeighborhoodId { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
