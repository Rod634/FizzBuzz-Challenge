using FizzBuzz.Services.Interfaces;
using System.Text.Json;

namespace FizzBuzz.Services
{
    public class GlitchService : IGlitchService
    {
        private readonly HttpClient _httpClient;

        public GlitchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string, int>> FetchAlternativeTokensFromApi()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "TwistedFizzBuzzApp/1.0");

            try
            {
                string response = await _httpClient.GetStringAsync("https://pie-healthy-swift.glitch.me/word");
                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (data == null || !data.ContainsKey("word") || !data.ContainsKey("number"))
                {
                    throw new FormatException("API Response format not valid");
                }

                string word = data["word"].ToString();
                int number = int.Parse(data["number"].ToString());

                return new Dictionary<string, int> { { word, number } };
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Fetch data API error", ex);
            }
        }
    }
}
