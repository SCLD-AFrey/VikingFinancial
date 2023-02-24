using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFinancial.Gui.Models.Services;

public class Files
{
    private readonly ILogger<Files> m_logger;
    private readonly Folders m_foldersService;

    public Files(Folders p_foldersService, ILogger<Files> p_logger)
    {
        m_foldersService = p_foldersService;
        m_logger = p_logger;

        m_logger.LogDebug("Initializing Files service");
    }

    public string LogFilePath => Path.Combine(m_foldersService.LogsDirectoryPath, "vikingfin.log");
    public string DocumentationFilePath => string.Empty;
    public string DatabaseFilePath => Path.Combine(m_foldersService.DatabaseDirectoryPath, "transaction.db");
    public string SettingsPath => Path.Combine(m_foldersService.DataDirectoryPath, "settings.ini");
}