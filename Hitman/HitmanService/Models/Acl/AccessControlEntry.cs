namespace HitmanService.Models.Acl
{
    public class AccessControlEntry
    {
        public Identity TargetIdentity { get; private set; }

        public IAccessControlResource Resource { get; private set; }

        public Operation Operation { get; private set; }

        public Permission Permission { get; private set; }
    }
}
