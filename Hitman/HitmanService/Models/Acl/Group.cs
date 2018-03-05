using System;
using System.Collections.Generic;

namespace HitmanService.Models.Acl
{
    public class Group : Identity
    {
        public Group(string uniqueName)
            : base(uniqueName)
        {
            Descendants = new List<Identity>();
        }
        public ICollection<Identity> Descendants { get; private set; }

        public override bool HasDescendant(Identity identity)
        {
            if (identity.UniqueName == UniqueName)
            {
                return true;
            }
            foreach (Identity descendant in Descendants)
            {
                if (descendant.HasDescendant(identity))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddDescendant(Identity identity)
        {
            if (HasDescendant(identity))
            {
                throw new InvalidOperationException($"Identity: {identity.UniqueName} is already a descendant of {UniqueName}.");
            }
            Descendants.Add(identity);
        }
    }
}
