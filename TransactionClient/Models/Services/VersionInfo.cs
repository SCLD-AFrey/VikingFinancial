using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models.Services;

public class VersionInfo
{
    private readonly ILogger<VersionInfo> m_logger;
    
    public VersionInfo(ILogger<VersionInfo> p_logger)
    {
        m_logger = p_logger;
        
        m_logger.LogDebug("Initializing VersionInfo service");
    }
    
    public string FileVersion => GetFileVersion() ?? string.Empty;

    private string? GetFileVersion()
    {
        m_logger.LogDebug("Retrieving version info from the application assembly");

        var assembly    = Process.GetCurrentProcess().MainModule!.FileName;
        var fileVersion = FileVersionInfo.GetVersionInfo(assembly);
        return fileVersion.FileVersion;
    }
}