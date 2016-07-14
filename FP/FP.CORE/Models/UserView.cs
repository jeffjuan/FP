using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FP.CORE.Models
{
    public class UserView
    {
        public FP_USER User { get; set; }

        public List<SelectListItem> Department { get; set; }
    }
}
