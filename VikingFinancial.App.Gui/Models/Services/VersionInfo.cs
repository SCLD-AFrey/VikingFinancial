using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Services
{
    public class VersionInfo
    {
        private readonly ILogger<VersionInfo> m_logger;
        public VersionInfo(ILogger<VersionInfo> p_logger)
        {
            m_logger = p_logger;
        
            m_logger.LogDebug("Initializing VersionInfo service");
        }
        public string FileVersionString => GetFileVersionString() ?? string.Empty;

        private string GetFileVersionString(int p_level = 2)
        {
            var versionInfo = GetFileVersionInfo();

            return p_level switch
            {
                1 => $"{versionInfo.FileMajorPart}",
                2 => $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}",
                3 => $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}",
                _ => throw new ArgumentOutOfRangeException(nameof(p_level), p_level, null)
            };
        }
        public FileVersionInfo GetFileVersionInfo()
        {
            m_logger.LogDebug("Retrieving version info from the application assembly");
        
            var assembly    = Assembly.GetExecutingAssembly();
            var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion;
        }
    }
}
