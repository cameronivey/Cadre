using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cadre.Email
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var nameValues = new Dictionary<string, string>();
            nameValues.Add("Name", "hi");
            var Name = new FormUrlEncodedContent(nameValues);
            client.GetAsync("http://localhost:50123/api/email/sendemail").ContinueWith(task =>
            {
                var responseNew = task.Result;
                Console.WriteLine(responseNew.Content.ReadAsStringAsync().Result);
            });

            Console.ReadKey();
        }
    }
}
