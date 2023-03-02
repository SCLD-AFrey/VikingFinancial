using System.IO;
using Microsoft.Extensions.Logging;

namespace TransactionNavigator.Models.Services;

public class Files
{
    private readonly ILogger<Files> m_logger;
    private readonly Folders        m_foldersService;

    public Files(Folders p_foldersService, ILogger<Files> p_logger)
    {
        m_foldersService = p_foldersService;
        m_logger         = p_logger;

        m_logger.LogDebug("Initializing Files service");
    }

    public string LogFilePath => Path.Combine(m_foldersService.LogsDirectoryPath, "navigator.log");
    public string DocumentationFilePath => string.Empty;
}