using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace VikingFinancial.Gui.Models.Services;

public class Folders
{
    private readonly ILogger<Folders> m_logger;

    public Folders(ILogger<Folders> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Initializing Folders service");

        CreateRequiredFolders();
    }

#if DEBUG
    public string DataDirectoryPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "Forge Data", "debug");
#else
    public string DataDirectoryPath => 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                     "Forge Data");
#endif

    public string LogsDirectoryPath => Path.Combine(DataDirectoryPath, "logs");
    public string DatabaseDirectoryPath => Path.Combine(DataDirectoryPath, "db");

    public void CreateRequiredFolders()
    {
        Directory.CreateDirectory(DataDirectoryPath);
        Directory.CreateDirectory(LogsDirectoryPath);
        Directory.CreateDirectory(DatabaseDirectoryPath);
    }
}