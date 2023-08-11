using System.Text.Json;
using API.Helpers;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header) 
        {
            // We need to convert our response in JSON format and camel case from Controller
            var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            // Since this is customized header, we need to explicitly allow CORS policy
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }   
    }
}