using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NumberSorting.Tests.Base;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace NumberSorting.Tests
{
    public class UnitTest1 : IClassFixture<RestLibrary>
    {
        private readonly ITestOutputHelper _output;
       private readonly RestClient _client;

        public UnitTest1(ITestOutputHelper output, RestLibrary restLibrary)
        {
            _output = output;
            _client = restLibrary.RestClient;
           
        }
        [Fact]
        public async Task ReadTest()
        {
          
            //rest request
            var request = new RestRequest("numbers");

            //rest response
            var response = await _client.GetAsync(request);

            //assert
            Assert.NotNull(response);
            _output.WriteLine(response.Content);
        }

        [Fact]
        public async Task PostTest()
        {

            var numbers = new[] { 89, 85, 21, 22, 36, 5, 72 };
            

            //rest request
            var request = new RestRequest("numbers");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(numbers);

            //rest response
            var response = await _client.PostAsync(request);

            //assert
            Assert.NotSame(numbers,response.Content);
            _output.WriteLine(response.Content);

        }
    }
}