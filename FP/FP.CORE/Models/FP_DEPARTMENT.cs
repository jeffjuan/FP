using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Models
{
    public class FP_DEPARTMENT
    {
        [Key]
        [Required]
        [DisplayName("ID")]
        public Guid ID { get; set; }


        [Required]
        [StringLength(35)]
        [DisplayName("部門名稱")]
        public string NAME { get; set; }


        [Required]
        [StringLength(10)]
        [DisplayName("部門代碼")]
        public string CODE { get; set; }

        //public virtual ICollection<FP_USER> Users { get; set; }

        public FP_DEPARTMENT()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
