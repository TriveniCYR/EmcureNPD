using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace EmcureNPD.Web.Filters
{
    public class CustomException : Exception
    {
        protected CustomException(SerializationInfo info,
      StreamingContext context) : base(info, context)
        {
        }

        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
