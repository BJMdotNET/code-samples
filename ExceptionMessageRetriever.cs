using System;
using System.Text;

namespace Samples.Common.Exceptions
{
    public static class ExceptionMessageRetriever
    {
        public static string Execute(Exception exc)
        {
            var exceptionMessage = new StringBuilder();

            var currentException = exc;
            exceptionMessage.Append(currentException.Message);

            while (currentException.InnerException != null)
            {
                exceptionMessage.AppendLine().Append(currentException.Message);
                currentException = currentException.InnerException;
            }

            return exceptionMessage.ToString();
        }
    }
}
