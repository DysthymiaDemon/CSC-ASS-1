using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Task3.Models
{
    public interface IGoogleRecaptchaService
    {
        Task<bool> Verify(String _Token);
    }


    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        public GoogleRecaptchaService Create()
        {
            return new GoogleRecaptchaService();
        }

        public virtual async Task<bool> Verify(String _Token)
        {
            GoogleRecaptchaData _myData = new GoogleRecaptchaData
            {
                response = _Token,
                secret = "6LdPPKMZAAAAAFGxuPMdEiQ2qBZPAAebwdEX8yVw"
            };

            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                {"secret", _myData.secret },
                {"response", _myData.response }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            var captcharesponsejson = await response.Content.ReadAsStringAsync();
            var capcharesponse = JsonConvert.DeserializeObject<GoogleResponse>(captcharesponsejson);

            return capcharesponse.success && capcharesponse.score > 0.5;
        }

    }

    public class GoogleRecaptchaData
    {
        public string response { get; set; }//token
        public string secret { get; set; }
    }

    public class GoogleResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}