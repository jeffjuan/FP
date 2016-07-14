using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Models
{
    public class FP_ROLE
    {
        [Key]
        [Required]
        [DisplayName("ID")]
        public Guid ID { get; set; }

        [DisplayName("角色代碼")]
        [StringLength(2)]
        public string ROLECODE { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("角色名稱")]
        public string NAME { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("中文角色名稱")]
        public string CNAME { get; set; }

        public FP_ROLE()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
