namespace Fancy.Domain
{
    public interface IFancyService
    {
        long GetFibonacciNumberForPositiveIndex(long index);
        long GetFibonacciNumberForNegativeIndex(long index);
        string GetTriangleType(int? a, int? b, int? c);
        string GetReverseWords(string sentence);
        string GetToken();
    }
}