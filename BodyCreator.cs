using System.IO;
using System.Reflection;
using System.Text;

namespace Samples.Logic.Emails.Tools
{
    internal class BodyCreator
    {
        private readonly EmailConfiguration _emailConfiguration;

        public BodyCreator(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public string Execute(EmailMessage emailMessage)
        {
            var body = new StringBuilder(GetTemplate(emailMessage.TemplateName));

            if (emailMessage.Values != null)
            {
                foreach (var kvp in emailMessage.Values)
                {
                    var value = (kvp.Value == null) ? string.Empty : kvp.Value.ToString();
                    body.Replace("{" + kvp.Key + "}", value);
                }
            }

            return body.ToString();
        }

        private string GetTemplate(string templateName)
        {
            var resourceName = string.Format("Strateloc.Logic.Emails.Templates.{0}.html", templateName);
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InfrabelException("Missing template: {0}", resourceName);
                }

                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
