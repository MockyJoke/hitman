using HitmanModel.Acl;
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

            Assert.True(group_root.HasDescendant(group_root));
            Assert.True(group_root.HasDescendant(user_a));
            Assert.True(group_root.HasDescendant(user_b));
            Assert.True(group_root.HasDescendant(group_a));
            Assert.True(group_a.HasDescendant(user_a));
            Assert.True(user_a.HasDescendant(user_a));
            Assert.False(group_a.HasDescendant(user_b));
            Assert.False(user_a.HasDescendant(user_b));
        }

        [Fact]
        public void TestAllHasAccess()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            InMemoryAccessManager accessManager = new InMemoryAccessManager();
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "Key1"
            };
            accessManager.CreateAccessControlContainer(sampleResource, user_a);
            AccessControlEntry accessControlEntry = new AccessControlEntry(Permission.Allow, Operation.Read, sampleResource, group_root);
            accessManager.AddEntry(sampleResource, accessControlEntry, user_a);
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_a));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_root));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, user_b));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, user_a));
        }

        [Fact]
        public void TestExplicitDenyGroup()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            InMemoryAccessManager accessManager = new InMemoryAccessManager();
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "Key1"
            };
            accessManager.CreateAccessControlContainer(sampleResource, user_a);

            AccessControlEntry accessControlEntry = new AccessControlEntry(Permission.Allow, Operation.Read, sampleResource, group_root);
            accessManager.AddEntry(sampleResource, accessControlEntry, user_a);

            AccessControlEntry denyEntry = new AccessControlEntry(Permission.Deny, Operation.Read, sampleResource, group_a);
            accessManager.AddEntry(sampleResource, denyEntry, user_a);

            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_root));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, user_b));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, group_a));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, user_a));
        }

        [Fact]
        public void TestExplicitDenyUser()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            InMemoryAccessManager accessManager = new InMemoryAccessManager();
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "Key1"
            };
            accessManager.CreateAccessControlContainer(sampleResource, user_a);

            AccessControlEntry accessControlEntry = new AccessControlEntry(Permission.Allow, Operation.Read, sampleResource, group_root);
            accessManager.AddEntry(sampleResource, accessControlEntry, user_a);

            AccessControlEntry denyEntry = new AccessControlEntry(Permission.Deny, Operation.Read, sampleResource, user_a);
            accessManager.AddEntry(sampleResource, denyEntry, user_a);

            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_root));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, user_b));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_a));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, user_a));
        }

        [Fact]
        public void TestExplicitAllowUser()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            InMemoryAccessManager accessManager = new InMemoryAccessManager();
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "Key1"
            };
            accessManager.CreateAccessControlContainer(sampleResource, user_a);

            AccessControlEntry accessControlEntry = new AccessControlEntry(Permission.Deny, Operation.Read, sampleResource, group_root);
            accessManager.AddEntry(sampleResource, accessControlEntry, user_a);

            AccessControlEntry denyEntry = new AccessControlEntry(Permission.Allow, Operation.Read, sampleResource, user_a);
            accessManager.AddEntry(sampleResource, denyEntry, user_a);

            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, group_root));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, user_b));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, group_a));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, user_a));
        }

        [Fact]
        public void TestDefaultPermission()
        {
            User user_a = new User("User_A");
            User user_b = new User("User_B");

            Group group_root = new Group("Group_root");
            group_root.AddDescendant(user_b);

            Group group_a = new Group("Group_A");
            group_a.AddDescendant(user_a);
            group_root.AddDescendant(group_a);

            InMemoryAccessManager accessManager = new InMemoryAccessManager();
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "Key1"
            };
            accessManager.CreateAccessControlContainer(sampleResource, user_a);

            AccessControlEntry allowEntry = new AccessControlEntry(Permission.Allow, Operation.Read, sampleResource, group_a);
            accessManager.AddEntry(sampleResource, allowEntry, user_a);

            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, group_root));
            Assert.False(accessManager.ValidateAccess(sampleResource, Operation.Read, user_b));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, group_a));
            Assert.True(accessManager.ValidateAccess(sampleResource, Operation.Read, user_a));
        }
    }
}
