using Microsoft.Extensions.Logging;

namespace VikingFinancial.TransactionServer.Services
{
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

        // TODO: Point to actual docs path. - Comment by Matt Heimlich on 07/13/2022@13:19:02
        public string DocumentationFilePath => string.Empty;
        public string DatabaseFilePath => Path.Combine(m_foldersService.DatabaseDirectoryPath, "transaction.db");
    }
}
