using System.Globalization;

namespace test_library.Utils
{
    public class CustomException: Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }

        public CustomException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
