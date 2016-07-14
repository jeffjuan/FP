using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Models
{
    public class FP_FEATURE
    {
        [Key]
        [Required]
        [DisplayName("ID")]
        public Guid ID { get; set; }

        [Required]
        [StringLength(35)]
        [DisplayName("作業名稱")]
        public string NAME { get; set; }

        [StringLength(35)]
        [DisplayName("作業代碼")]
        public string CODE { get; set; }


        [StringLength(35)]
        [DisplayName("控制器名稱")]
        public string CONTROLLER { get; set; }

        [StringLength(255)]
        [DisplayName("權限集合")]
        public string PERMISSION { get; set; }

        [StringLength(1)]
        [DisplayName("是否為選單")]
        public string ISMENU { get; set; }


        [StringLength(100)]
        [DisplayName("父節點")]
        public string PARENT { get; set; }

        [StringLength(255)]
        [DisplayName("連結")]
        public string URL { get; set; }

        public FP_FEATURE()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
