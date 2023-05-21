using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public record UserResponse(int Id, string Login, bool CanChangeEntities);
}
