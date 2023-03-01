using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models.Services
{
    public class Folders
    {
        private readonly ILogger<Folders> m_logger;
        public Folders(ILogger<Folders> p_logger)
        {
            m_logger = p_logger;

            m_logger.LogDebug("Initializing Folders service");

            CreateRequiredFolders();
        }
        
        public string ProgramDataDirectoryPath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                ".VikingTransactionClient");
        
        
        public string LogsDirectoryPath => Path.Combine(ProgramDataDirectoryPath, "logs");
        public void CreateRequiredFolders()
        {
            Directory.CreateDirectory(ProgramDataDirectoryPath);
            Directory.CreateDirectory(LogsDirectoryPath);
        }
    }
}
