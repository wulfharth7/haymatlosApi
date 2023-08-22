using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace haymatlosApi.haymatlosApi.Utils
{
    public static class NullChecker
    {
        public static HttpRequestMessage? Request { get; private set; }

        public static bool IsNull(object obj)
        {
            if (obj == null) return true;
            else return false;
        }

        public static async Task<HttpResponseMessage> IsNullOrUndefinedAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent("User name or password is incorrect", Encoding.UTF8),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            Console.WriteLine(token);
            return new HttpResponseMessage
            {
                Content = new StringContent(token, Encoding.UTF8),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}

//will add functionalities for checking registration.