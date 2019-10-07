using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHookTesterApi.Models;

namespace WebHookTesterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookSubscriber2Controller : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public async Task PostAsync([FromBody] WebHookTesterApi.Models2.RootObject value)
        {
            var pullCommentUrl = value.pull_request.review_comments_url;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = "455489af7ab6051fedadc97eca26e2412760e2be";
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);



            var response = await client.GetAsync("repos/tavisca-schawla/university-management-app/issues/8/comments");
            string data = "";
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                data = "Error In Fetching Data fro api call";
            }
            System.IO.File.WriteAllText(@"C:\Sundesh\Training\WebHookTesting\WebHookTesterApi\response.txt", data);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}