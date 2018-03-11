namespace HitmanModel.Acl
{
    public abstract class AceessManager
    {
        public abstract AccessControlContainer GetAccessControlContainer(IAccessControlResource resource);
        public abstract void CreateAccessControlContainer(IAccessControlResource resource, Identity requester);
        public abstract void AddEntry(IAccessControlResource resource, AccessControlEntry entry, User requester);
        public abstract void DeleteEntry(IAccessControlResource resource, AccessControlEntry entry, User requester);
        public virtual bool ValidateAccess(IAccessControlResource resource, Operation operation, Identity requester)
        {
            bool premissionGranted = false;
            AccessControlContainer accessControlContainer = GetAccessControlContainer(resource);
            foreach (AccessControlEntry ace in accessControlContainer.Entries)
            {
                if (ace.TargetIdentity.HasDescendant(requester))
                {
                    if (ace.Permission == Permission.Deny)
                    {
                        return false;
                    }
                    premissionGranted = true;
                }
            }
            return premissionGranted;
        }
    }
}
