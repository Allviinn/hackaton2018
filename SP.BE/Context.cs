using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.BE
{
    public class Context
    {
        public int? IdUser { get; set; }
        public List<string> ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
