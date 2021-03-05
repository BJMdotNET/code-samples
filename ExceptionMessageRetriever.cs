using System;
using System.Text;

namespace Samples.Common.Exceptions
{
    public static class ExceptionMessageRetriever
    {
        public static string Execute(Exception exc)
        {
            var messages = new List<string>();
            do
            {
                messages.Add(exc.Message);
                exc = exc.InnerException;
            }
            while (exc != null);

            return string.Join(" - ", messages);
        }
    }
}
