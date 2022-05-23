using System;
using RestSharp;

namespace NumberSorting.Tests.Base;

public class RestLibrary
{
    
    public RestLibrary()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("http://localhost:5265/"),
            RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true

        };

        RestClient = new RestClient(restClientOptions);
    }

    public RestClient RestClient { get;}
}