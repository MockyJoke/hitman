using System;
using System.Collections.Generic;

namespace HitmanModel.Acl
{
    public class InMemoryAccessManager : AceessManager
    {
        private Dictionary<String, AccessControlContainer> accessDict = new Dictionary<string, AccessControlContainer>();
        public override AccessControlContainer GetAccessControlContainer(IAccessControlResource resource)
        {
            if (!accessDict.ContainsKey(resource.Identifier))
            {
                throw new InvalidOperationException($"AccessControlContainer does not exist for resource: {resource.Identifier}");
            }
            return accessDict[resource.Identifier];
        }

        public override void CreateAccessControlContainer(IAccessControlResource resource, Identity owner)
        {
            if (accessDict.ContainsKey(resource.Identifier))
            {
                throw new InvalidOperationException($"AccessControlContainer already exist for resource: {resource.Identifier}");
            }
            accessDict[resource.Identifier] = new AccessControlContainer(owner);
        }

        public override void AddEntry(IAccessControlResource resource, AccessControlEntry entry, User requester)
        {
            EnsureCanEdit(resource, requester);
            accessDict[resource.Identifier].Entries.Add(entry);
        }

        public override void DeleteEntry(IAccessControlResource resource, AccessControlEntry entry, User requester)
        {
            EnsureCanEdit(resource, requester);
            accessDict[resource.Identifier].Entries.Remove(entry);
        }
      
        private void EnsureCanEdit(IAccessControlResource resource, User opertor)
        {
            AccessControlContainer acc = GetAccessControlContainer(resource);
            if (!acc.Owner.HasDescendant(opertor))
            {
                throw new InvalidOperationException($"{opertor.UniqueName} is not authorize to edit the ACL for resource: {resource.Identifier}");
            }
        }
    }
}
