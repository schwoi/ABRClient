using System;
using System.Linq;
using AbrApi.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;
using Xunit;

namespace AbrApi.Tests
{
    public class ClientTests
    {
        Guid ApiKey = new Guid("{NEEDS API GUID");
        [Fact]
        public async void ShouldBeAbleToGetAbn()
        {
            var client = new AbrApi.Client(ApiKey);
            var pssAbn = "74172177893";
            var result = await client.AbnSearch(pssAbn);
            result.EntityName.ShouldBe("THE TRUSTEE FOR PSS FUND");
        }
        
        [Fact]
        public async void ShouldBeAbleToGetAcn()
        {
            var client = new AbrApi.Client(ApiKey);
            var bunningsAcn = "008672179";
            var result = await client.AcnSearch(bunningsAcn);
            result.Abn.ShouldBe("008672179");
            result.EntityName.ShouldBe("BUNNINGS GROUP LIMITED");
        }
        
        [Fact]
        public async void ShouldBeAbleToGetSearchResults()
        {
            var client = new AbrApi.Client(ApiKey);
            var bunnings = "BUNNINGS GROUP LIMITED";
            var result = await client.NameSearch(bunnings);
            result.Names.Count.ShouldBePositive();
            result.Names.First(x => x.Score == 100).Abn.ShouldBe("26008672179");
        }

        [Fact]
        public async void ShouldThrowAnInvalidApiKeyException()
        {
            var client = new AbrApi.Client(Guid.Empty);
            var bunnings = "BUNNINGS GROUP LIMITED";

            Should.Throw<InvalidApiKeyException>(() =>
            {
                var y = client.NameSearch(bunnings).GetAwaiter().GetResult();
            });

        }
    }
}