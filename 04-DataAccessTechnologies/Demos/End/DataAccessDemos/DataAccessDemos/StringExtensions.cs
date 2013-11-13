
namespace DataAccessDemos
{
  public static class StringExtensions
  {
    public static bool HasEvenLength(this string Source)
    {
      return Source.Length % 2 == 0;
    }
  }
}


    //public static bool IsLengthMultipleOf(this string Source, double Multiple)
    //{
    //  return Source.Length % 2 == 0;
    //}
