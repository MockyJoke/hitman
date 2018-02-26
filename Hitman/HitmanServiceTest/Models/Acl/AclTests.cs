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
            User user = new User()
            {
                UniqueName = "TestUser"
            };
            SampleResource sampleResource = new SampleResource()
            {
                Identifier = "01"
            };

        }
    }
}
