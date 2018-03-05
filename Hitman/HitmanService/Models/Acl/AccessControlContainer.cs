using System.Collections.Generic;

namespace HitmanService.Models.Acl
{
    public class AccessControlContainer
    {
        public Identity Owner { get; private set; }
        public List<AccessControlEntry> Entries { get; private set; }
        public AccessControlContainer(Identity owner)
        {
            Owner = owner;
            Entries = new List<AccessControlEntry>();
        }
    }
}
