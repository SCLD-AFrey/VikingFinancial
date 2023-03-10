using System;
using System.IO;

namespace TransactionNavigatorGui.Services.Infrastructure
{
    public class CommonFiles
    {
        private readonly CommonDirectories m_commonDirectories;
    
        public CommonFiles(CommonDirectories p_commonDirectories)
        {
            m_commonDirectories = p_commonDirectories;
        
            ClientLogsPath = Path.Combine(m_commonDirectories.ServerDataPath, "logs", "client.log");
            SettingsPath   = Path.Combine(m_commonDirectories.ServerDataPath, "settings", "settings.ini");
            DataBasePath   = Path.Combine(m_commonDirectories.ServerDataPath, "db", "transaction.db");
            UsersDatabasePath   = Path.Combine(m_commonDirectories.ServerDataPath, "db", "users.db");
            Console.WriteLine("CommonFiles");
        
            CreateNecessaryDirectories();
        }
    
        public string UsersDatabasePath { get; }
        public string DataBasePath    { get; }
        public string ClientLogsPath    { get; }
        public string SettingsPath      { get; }

        private void CreateNecessaryDirectories()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ClientLogsPath)    ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)      ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(DataBasePath)      ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(UsersDatabasePath)      ?? string.Empty);
        }
    }
}