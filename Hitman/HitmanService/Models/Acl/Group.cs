using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HitmanService.Models.Acl
{
    public class Group : Identity
    {
        public ICollection<Identity> ChildIdentities { get; private set; }
    }
}
