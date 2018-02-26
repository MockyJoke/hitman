namespace HitmanService.Models.Acl
{
    /// <summary>
    /// The fundamental element of the access control system.
    /// </summary>
    public abstract class Identity
    {
        public string UniqueName { get; protected set; }
    }
}
