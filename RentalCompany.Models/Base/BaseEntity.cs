using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCompany.Models.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid? CreatedBy { get; set; }


    }
}
