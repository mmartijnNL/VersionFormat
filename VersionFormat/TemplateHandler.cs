namespace VersionFormat
{
    public class TemplateHandler
    {
        public static string GetVersionFromTemplate(string template)
        {
            string version = template;

            // Resolve our own sections
            version = version.Replace("{major}", "1");
            version = version.Replace("{minor}", "0");
            version = version.Replace("{build}", "0");
            version = version.Replace("{revision}", "0");

            var sections = version.StringSections();

            // Resolve all other sections using the date time now
            var now = DateTime.Now;
            foreach (var section in sections)
            {
                var trimmedSection = section.Replace("{", "").Replace("}", "");
                version = version.Replace(section, now.ToString(trimmedSection));
            }

            return version;
        }
    }
}