using System.Threading.Tasks;

namespace VersionFormat
{
    internal static class ProjectExtensions
    {
        private const string VersionFormat = "VersionFormat";
        private const string Version = "Version";
        private const string DefaultTemplate = "{major}.{minor}.{build}.{revision}";

        public static async Task<string> EnableVersionFormatAsync(this Project prj)
        {
            await prj.SetVersionTemplateAsync(DefaultTemplate);
            return DefaultTemplate;
        }

        public static async Task<string> GetVersionAsync(this Project prj)
        {
            return await prj.GetAttributeAsync(Version);
        }

        public static async Task<string> GetVersionTemplateAsync(this Project prj)
        {
            return await prj.GetAttributeAsync(VersionFormat);
        }

        public static Task<bool> SetVersionAsync(this Project prj, string version)
        {
            return prj.TrySetAttributeAsync(Version, version);
        }

        public static Task<bool> SetVersionTemplateAsync(this Project prj, string template)
        {
            return prj.TrySetAttributeAsync(VersionFormat, template);
        }
    }
}