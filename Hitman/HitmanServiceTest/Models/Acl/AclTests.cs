using HitmanService.Models.Acl;
using Xunit;

namespace HitmanServiceTest.Models.Acl
{
    public class AclTests
    {
        private class SampleResource : IAccessControlResource
        {
            public string Identifier { get; set; }
        }
        [Fact]
        public void TestSingleUser()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            Assert.True(group_root.HasDescendant(user_a));
            Assert.True(group_root.HasDescendant(user_b));
            Assert.True(group_root.HasDescendant(group_a));
            Assert.True(group_a.HasDescendant(user_a));
            Assert.False(group_a.HasDescendant(user_b));
        }
    }
}
