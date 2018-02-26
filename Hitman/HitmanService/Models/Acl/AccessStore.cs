using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HitmanService.Models.Acl
{
    public class AccessStore
    {
        private Dictionary<String, List<AccessControlEntry>> accessDict = new Dictionary<string, List<AccessControlEntry>>();
        public bool SpecifyAccessRule(IAccessControlResource resource, AccessControlEntry entry)
        {
            if (!accessDict.ContainsKey(resource.Identifier))
            {
                accessDict[resource.Identifier] = new List<AccessControlEntry>();
            }
            return false;
        }
    }
}
