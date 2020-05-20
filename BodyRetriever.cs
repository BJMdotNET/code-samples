using AdfsMfa.Shared.Languages;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Samples.Core.SingleUseCodes.Emails
{
    internal class BodyRetriever
    {
        public static string Execute(string singleUseCode)
        {
            return GetTemplate().Replace("{code}", singleUseCode);
        }

        private static string GetTemplate()
        {
            // ReSharper disable once PossibleNullReferenceException
            var resourceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." +
                               $"EmailBody.html";
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    Trace.TraceError($"Missing template: '{resourceName}'.");
                    throw new Exception($"Missing template: {resourceName}");
                }

                using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
