namespace HitmanService.Models.Acl
{
    /// <summary>
    /// The fundamental element of the access control system.
    /// </summary>
    public abstract class Identity
    {
        public Identity(string uniqueName)
        {
            UniqueName = uniqueName;
        }
        public string UniqueName { get; protected set; }
        public abstract bool HasDescendant(Identity identity);
    }
}
