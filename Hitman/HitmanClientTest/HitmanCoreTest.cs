using HitmanClient;
using HitmanModel.Client;
using HitmanModel.Storage;
using System.Threading.Tasks;
using Xunit;

namespace HitmanClientTest
{
    public class HitmanCoreTest
    {
        public class TestData
        {
            public string StringData { get; set; }
        }
        [Fact]
        public async Task TestSaveLoadObjectAsync()
        {
            TestData testData = new TestData()
            {
                StringData = "Hello world!"
            };
            //IHitmanClient hitmanClient = new HitmanCoreClient(new HitmanCoreConfig() { Host="localhost:51195"});
            IHitmanClient hitmanClient = new HitmanCoreClient(new HitmanCoreConfig() { Host = "hitmanservice.azurewebsites.net" });
            StorableIdentifier identifier = new StorableIdentifier()
            {
                Category = "Test",
                UniqueName = "01"
            };
            await hitmanClient.SaveAsync(testData, identifier);
            TestData result = await hitmanClient.LoadAsync<TestData>(identifier);
            Assert.Equal(testData.StringData, result.StringData);
        }
    }
}
