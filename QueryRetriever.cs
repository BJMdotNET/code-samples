using System;
using System.IO;
using System.Reflection;

namespace Samples.Common.Database
{
    public static class QueryRetriever
    {
        public static string GetQuery(MethodBase methodBase, string scriptName)
        {
            var namespacePrefix = methodBase.DeclaringType == null
                ? string.Empty
                : methodBase.DeclaringType.Namespace;

            return GetQuery(namespacePrefix + ".", scriptName);
        }

        private static string GetQuery(string namespacePrefix, string scriptName)
        {
            var resourceName = namespacePrefix + scriptName;
            var assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                throw new Exception($"Could not get EntryAssembly.");
            }

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Missing SQL script: {resourceName}.");
                }

                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
