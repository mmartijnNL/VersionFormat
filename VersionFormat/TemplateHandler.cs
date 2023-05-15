namespace VersionFormat
{
    public class TemplateHandler
    {
        /// <summary>
        /// Generates a version based on the given template. Uses DateTime.ToString to format datetimes (e.g. "{yyyy}" -> "2023").
        /// You choose to ignore the the daylight savings by adding -DST (e.g. "{-DSTHH:mm}" -> "08:42").
        /// </summary>
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
            var nowIgnoreDaylightSavings = DateTime.Now.ToUniversalTime() + TimeZoneInfo.Local.BaseUtcOffset;
            foreach (var section in sections)
            {
                var trimmedSection = section.Replace("{", "").Replace("}", "");
                if (trimmedSection.StartsWith("-DST"))
                {
                    trimmedSection = trimmedSection.Substring(4);
                    version = version.Replace(section, nowIgnoreDaylightSavings.ToString(trimmedSection));
                }
                else
                {
                    version = version.Replace(section, now.ToString(trimmedSection));
                }
            }

            return version;
        }
    }
}