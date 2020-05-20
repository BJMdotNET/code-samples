using System;
using System.Linq;

namespace Samples.Common.Logging
{
    public static class ClassNameRetriever
    {
		// usage ClassNameRetriever.Execute(MethodBase.GetCurrentMethod())
        public static string Execute(Type nameSpace)
        {
            return nameSpace == null ? string.Empty : nameSpace.ToString().Split('.').Last();
        }
    }
}
