namespace HitmanModel.Acl
{
    public class AccessControlEntry
    {
        public Identity TargetIdentity { get; private set; }

        public IAccessControlResource Resource { get; private set; }

        public Operation Operation { get; private set; }

        public Permission Permission { get; private set; }

        public AccessControlEntry(Permission permission, Operation operation, IAccessControlResource resource, Identity targetIdentity)
        {
            TargetIdentity = targetIdentity;
            Resource = resource;
            Operation = operation;
            Permission = permission;
        }
    }
}
