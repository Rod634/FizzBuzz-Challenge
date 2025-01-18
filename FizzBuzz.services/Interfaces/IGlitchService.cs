namespace FizzBuzz.Services.Interfaces
{
    public interface IGlitchService
    {
        public Task<Dictionary<string, int>> FetchAlternativeTokensFromApi();
    }
}
