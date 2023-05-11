global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;
using System.Threading;

namespace VersionFormat
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.VersionFormatString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class VersionFormatPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            VS.Events.BuildEvents.ProjectBuildStarted += SetVersion;
        }

        private void SetVersion(Project prj)
        {
            JoinableTaskFactory.Run(async () => await SetVersionAsync(prj));
        }

        private async Task SetVersionAsync(Project prj)
        {
            var versionTemplate = await prj.GetVersionTemplateAsync();
            if (!string.IsNullOrEmpty(versionTemplate))
            {
                await prj.SetVersionAsync(TemplateHandler.GetVersionFromTemplate(versionTemplate));
            }
        }
    }
}