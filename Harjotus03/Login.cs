using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjotus03
{
    public class Login
    {
        [Key]
        public string? pincode { get; set; }
        public string? nickname { get; set; }
    }
}
