using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CF.RESTClientDotNet;
using Newtonsoft.Json;

//using RestSharp;
namespace avalanchain
{
    public class RestService 
    {
        //HttpClient client;
        //   // ...

        //public RestService()
        //{
        //    client = new HttpClient();
        //    client.MaxResponseContentBufferSize = 256000;
        //}
        //...
        public static async Task<string> DownloadData2(string url)
        {
            return await Task.FromResult("TODO: Uncomment code!!!");
            //using (var client = new HttpClient())
            //{
            //    using (var r = await client.GetAsync(new Uri(url)).ConfigureAwait(false))
            //    {
            //        string res = await r.Content.ReadAsStringAsync();
            //        string result = r.Content.ReadAsStringAsync().Result;//.ReadAsStringAsync();
            //        return result;
            //    }
            //}
        }
        public static async Task<string> DownloadData(string url)
        {
            //var restClient = new RESTClient(new NewtonsoftSerializationAdapter(), new Uri(url));
            //var result = await restClient.GetAsync(); //GetAsync<groupktResult<CountriesResult>>();
            var restClient = new RESTClient(new NewtonsoftSerializationAdapter(), new Uri(url));
            var result = await restClient.GetAsync(); //GetAsync<groupktResult<CountriesResult>>();
            var data = result.Data;
            string res = System.Text.Encoding.UTF8.GetString(data);
            return res;
        }
    }
    public class NewtonsoftSerializationAdapter : ISerializationAdapter
    {
        public async Task<object> DeserializeAsync(byte[] binary, Type type)
        {
            var markup = await EncodeStringAsync(binary);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject(markup, type));
        }

        public async Task<T> DeserializeAsync<T>(byte[] binary)
        {
            var markup = await EncodeStringAsync(binary);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(markup));
        }

        public async Task<byte[]> SerializeAsync<T>(T value)
        {
            var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value));
            var binary = await Task.Factory.StartNew(() => Encoding.UTF8.GetBytes(json));
            return binary;
        }

        public async Task<string> EncodeStringAsync(byte[] bytes)
        {
            return await Task.Factory.StartNew(() => Encoding.UTF8.GetString(bytes, 0, bytes.Length));
        }

        public async Task<byte[]> DecodeStringAsync(string theString)
        {
            return await Task.Factory.StartNew(() => Encoding.UTF8.GetBytes(theString));
        }
    }
}
