using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task<string> DownloadData(string url)
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
    }
}
