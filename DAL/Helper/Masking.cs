
namespace DAL.Helper
{
    public class Masking
    {
        private readonly int _visibleChars;

        public Masking(int visibleChars)
        {
            _visibleChars = visibleChars;
        }

        public string Mask(string input)
        {
            if (input.Length > _visibleChars)
            {
                var mask = new string('*', input.Length - _visibleChars);
                return mask + input.Substring(input.Length - _visibleChars);
            }
            else
            {
                return input;
            }
        }
    }

}
