namespace HitmanService.Models.Acl
{
    public class User : Identity
    {
        public User(string uniqueName)
            : base(uniqueName)
        {
        }
        public string Key { get; private set; }

        public override bool HasDescendant(Identity identity)
        {
            if (identity.UniqueName == UniqueName)
            {
                return true;
            }
            return false;
        }
    }
}
