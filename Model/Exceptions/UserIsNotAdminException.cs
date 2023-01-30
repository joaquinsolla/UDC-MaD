using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    public class UserIsNotAdminException : Exception
    {
        public UserIsNotAdminException(long usrId)
            : base("User " + usrId + " is not an admin")
        {
            this.usrId = usrId;
        }

        public long usrId { get; private set; }
    }
}
