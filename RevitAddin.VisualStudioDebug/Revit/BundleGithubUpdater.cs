using ricaun.Revit.Github;
using System;
using System.Threading.Tasks;

namespace RevitAddin.VisualStudioDebug.Revit
{
    public static class BundleGithubUpdater
    {
        private const string RepoOwner = "ricaun-io";
        private const string RepoName = "RevitAddin.VisualStudioDebug";
        private static string RepoUrl => $"https://github.com/{RepoOwner}/{RepoName}";

        public static void RunUpdate()
        {
            Task.Run(async () =>
            {
                var service = new GithubRequestService(RepoOwner, RepoName);
                bool downloadedNewVersion = await service.Initialize();
                if (downloadedNewVersion)
                {
                    ShowBalloon("Download New Release!", null, RepoUrl);
                    Console.WriteLine($"RevitAddin.VisualStudioDebug: {downloadedNewVersion}");
                }
            });
        }

        private static void ShowBalloon(string title, string category = null, string uriString = null)
        {
            if (title == null) return;
            Autodesk.Internal.InfoCenter.ResultItem ri = new Autodesk.Internal.InfoCenter.ResultItem();
            ri.Category = category ?? typeof(BundleGithubUpdater).Assembly.GetName().Name;
            ri.Title = title.Trim();
            if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out Uri uri))
                ri.Uri = uri;
            Autodesk.Windows.ComponentManager.InfoCenterPaletteManager.ShowBalloon(ri);
        }
    }

}