using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FP.CORE.Models
{
    public class FP_USER
    {
        [Key]
        [Required]
        [DisplayName("ID")]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "請輸入使用者帳號")]
        [DisplayName("使用者帳號")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ACCOUNT { get; set; }


        [Required(ErrorMessage = "請輸入員工編號")]
        [DisplayName("員工編號")]
        [StringLength(25)]
        public string CODE { get; set; }


        [Required]
        [DisplayName("中文姓名")]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public string CNAME { get; set; }


        [DisplayName("英文姓名")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string ENAME { get; set; }


        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
        [DisplayName("電子郵件")]
        [StringLength(50)]
        public string EMAIL { get; set; }


        [Required]
        [DisplayName("密碼")]
        [StringLength(255)]
        public string PW { get; set; }


        [Required]
        [DisplayName("使用者編號")]
        [StringLength(50)]
        public string USERNO { get; set; }

        // Foreign key
        [DisplayName("所屬部門代碼")]
        [StringLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string DEPARTMENTCODE { get; set; }


        [DisplayName("是否啟用")]
        [Required(ErrorMessage = "請輸入是否啟用")]
        public bool ENABLE { get; set; }


        [DisplayName("建立時間")]
        public DateTime? CreDateTime { get; set; }


        public FP_USER()
        {
            this.ID = Guid.NewGuid();
            this.ENABLE = false;
        }
    }
}
