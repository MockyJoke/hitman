using System;
using System.Collections.Generic;

namespace HitmanService.Models.Acl
{
    public class AccessManager
    {
        private Dictionary<String, AccessControlContainer> accessDict = new Dictionary<string, AccessControlContainer>();

        public AccessControlContainer GetAccessControlContainer(IAccessControlResource resource)
        {
            if (!accessDict.ContainsKey(resource.Identifier))
            {
                throw new InvalidOperationException($"AccessControlContainer does not exist for resource: {resource.Identifier}");
            }
            return accessDict[resource.Identifier];
        }

        public void CreateAccessControlContainer(IAccessControlResource resource, Identity owner)
        {
            if (accessDict.ContainsKey(resource.Identifier))
            {
                throw new InvalidOperationException($"AccessControlContainer already exist for resource: {resource.Identifier}");
            }
            accessDict[resource.Identifier] = new AccessControlContainer(owner);
        }

        public void AddEntry(IAccessControlResource resource, AccessControlEntry entry, User editor)
        {
            EnsureCanEdit(resource, editor);
            accessDict[resource.Identifier].Entries.Add(entry);
        }

        public void DeleteEntry(IAccessControlResource resource, AccessControlEntry entry, User editor)
        {
            EnsureCanEdit(resource, editor);
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
