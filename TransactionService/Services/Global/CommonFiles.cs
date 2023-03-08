namespace TransactionService.Services.Global
{
    public class CommonFiles
    {
        private readonly CommonDirectories m_commonDirectories;
    
        public CommonFiles(CommonDirectories p_commonDirectories)
        {
            m_commonDirectories = p_commonDirectories;
        
            WorkerLogsPath = Path.Combine(m_commonDirectories.ServerDataPath, "logs", "worker", "worker.log");
            HostLogsPath   = Path.Combine(m_commonDirectories.ServerDataPath, "logs", "host", "host.log");
            SettingsPath   = Path.Combine(m_commonDirectories.ServerDataPath, "settings", "settings.ini");
            DataBasePath   = Path.Combine(m_commonDirectories.ServerDataPath, "db", "transaction.db");
            Console.WriteLine("CommonFiles");
        
            CreateNecessaryDirectories();
        }
    
        public string UsersDatabasePath { get; }
        public string DataBasePath    { get; }
        public string WorkerLogsPath    { get; }
        public string HostLogsPath      { get; }
        public string SettingsPath      { get; }

        private void CreateNecessaryDirectories()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(WorkerLogsPath)    ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)      ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)      ?? string.Empty);
            Directory.CreateDirectory(Path.GetDirectoryName(DataBasePath)      ?? string.Empty);
        }
    }
}