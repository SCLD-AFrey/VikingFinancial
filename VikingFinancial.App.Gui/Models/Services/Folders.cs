using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Services;

public class Folders
{
    private readonly ILogger<Folders> m_logger;
    
    public Folders(ILogger<Folders> p_logger)
    {
        m_logger = p_logger;
        
        m_logger.LogDebug("Initializing Folders service");
        
        CreateRequiredFolders();
    }
    
    public string DataDirectoryPath => 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                     ".VikingFinAppGui");

    public string LogsDirectoryPath => Path.Combine(DataDirectoryPath, "logs");
    public string DatabaseDirectoryPath => Path.Combine(DataDirectoryPath, "db");
    
    public void CreateRequiredFolders()
    {
        Directory.CreateDirectory(DataDirectoryPath);
        Directory.CreateDirectory(LogsDirectoryPath);
        Directory.CreateDirectory(DatabaseDirectoryPath);
    }
}