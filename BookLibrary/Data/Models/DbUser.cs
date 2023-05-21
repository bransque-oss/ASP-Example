using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class DbUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool CanChangeEntities { get; set; }
    }
}
