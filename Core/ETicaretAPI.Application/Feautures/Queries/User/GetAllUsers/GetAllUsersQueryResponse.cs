using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public int TotatlCount { get; set; }
        public object Users { get; set; }
    }
}
