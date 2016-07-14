using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Models
{
    public class FP_USER_FEATURE_ROLE
    {
        [Key]
        [Required]
        [DisplayName("ID")]
        public Guid ID { get; set; }

        [Required]
        [DisplayName("使用者ID")]
        public Guid USER_ID { get; set; }

        [Required]
        [DisplayName("角色代碼")]
        [StringLength(2)]
        public string ROLE_CODE { get; set; }

        [Required]
        [StringLength(35)]
        [DisplayName("作業代碼")]
        public string FEATURE_CODE { get; set; }

        public FP_USER_FEATURE_ROLE()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
