namespace TwistedFizzBuzzLibrary.interfaces
{
    public interface ITwistedFizzBuzz
    {
        public IList<string> StandardFizzBuzz(int init, int final);
        public IList<string> NonSenquentialFIzzBuzz(IEnumerable<int> numberList);
        public IList<string> AlternaTiveTokens(Dictionary<string, int> alternativeTokens, int init, int final);
        public Task<IList<string>> AlternativeTokensByApi(int init, int final);
    }
}
