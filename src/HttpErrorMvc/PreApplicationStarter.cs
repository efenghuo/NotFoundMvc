using System.Web;

[assembly: PreApplicationStartMethod(typeof(HttpErrorMvc.PreApplicationStarter), "Start")]

namespace HttpErrorMvc
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    /// <summary>
    /// Runs at web application start.
    /// </summary>
    public static class PreApplicationStarter
    {
        private static bool started;

        public static void Start()
        {
            if (started)
            {
                // Only start once.
                return;
            }

            started = true;
            DynamicModuleUtility.RegisterModule(typeof(InstallerModule));
        }
    }
}