using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AbrApi.Exceptions;
using AbrApi.Models;
using Newtonsoft.Json;

namespace AbrApi
{
    public class Client
    {
        public Guid ApiKey { get; set; }
        private const string _contentType = "application/json";
        public string EndpointUrl => "https://abr.business.gov.au/json/";
        public bool IgnoreSslCertificate { get; set; }
        private HttpClient httpClient;

        public Client(Guid apiKey)
        {
            ApiKey = apiKey;
            CreateClient();
        }
        
        public Client(Guid apiKey, bool ignoreSslCertificate)
        {
            ApiKey = apiKey;
            IgnoreSslCertificate = ignoreSslCertificate;
            CreateClient();
        }
        
        private void CreateClient()
        {

            var httpClientHandler = new HttpClientHandler();

            if (IgnoreSslCertificate)
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            httpClient = new HttpClient(httpClientHandler);
            httpClient.BaseAddress = new Uri(EndpointUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));

        }

        public async Task<EntityDetail> AbnSearch(string abn)
        {
            var path = $"AbnDetails.aspx?abn={abn}";
            var response = await ExecuteGetCommandAsync(path);

            return JsonConvert.DeserializeObject<EntityDetail>(response.Result);
        }
        
        public async Task<EntityDetail> AcnSearch(string acn)
        {
            var path = $"AcnDetails.aspx?acn={acn}";
            var response = await ExecuteGetCommandAsync(path);

            return JsonConvert.DeserializeObject<EntityDetail>(response.Result);
        }
        
        public async Task<SearchResult> NameSearch(string name, int? count = null)
        {
            var path = $"MatchingNames.aspx?name={name}";
            if (count != null)
                path += "&maxResults=" + count.Value;
            
            var response = await ExecuteGetCommandAsync(path);

            return JsonConvert.DeserializeObject<SearchResult>(response.Result);
            
        }

        private async Task<JsonResponse> ExecuteGetCommandAsync(string path)
        {
            HasApiKey();
            var returnResponse = new JsonResponse();
            var response = await httpClient.GetAsync(path + $"&guid={ApiKey}");
            returnResponse.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                returnResponse.Result = result.Replace("callback(", "").TrimEnd(')');
            }
            else
            {
                returnResponse.Response = response.ReasonPhrase;
            }

            return returnResponse;
        }

        private void HasApiKey()
        {
            if (ApiKey == Guid.Empty)
                throw new InvalidApiKeyException("The API Key is needed to process requests");
        }
    }
}