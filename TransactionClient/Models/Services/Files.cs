using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.DataStructures;

namespace TransactionClient.Models.Services
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
        private string ClientConfigFilePath =>
            Path.Combine(m_foldersService.ProgramDataDirectoryPath, "config");

        public ClientConfiguration? GetClientConfig()
        {
            if ( !File.Exists(ClientConfigFilePath) ) return null;

            try
            {
                var configJson = JsonSerializer.Deserialize<ClientConfiguration>(File.ReadAllText(ClientConfigFilePath));
                if ( configJson is null )
                {
                    throw new Exception($"Trouble deserializing the file at '{ClientConfigFilePath}'");
                }

                return configJson;
            }
            catch
            {
                // Couldn't read file. Return null. - Comment by Matt Heimlich on 02/12/2022 @ 14:54:16
                return null;
            }
        }
        public async Task SaveClientConfig(string? p_serverAddress, string? p_port)
        {
            if ( string.IsNullOrWhiteSpace(p_serverAddress) ) return;

            try
            {
                if ( !File.Exists(ClientConfigFilePath) )
                {
                    var serializer = JsonSerializer.Serialize(new ClientConfiguration
                        { ServerAddress = p_serverAddress,  Port = p_port});
                    await File.WriteAllTextAsync(ClientConfigFilePath, serializer);
                }
                else
                {
                    var configJson = JsonSerializer.Deserialize<ClientConfiguration>(await File.ReadAllTextAsync(ClientConfigFilePath));
                    if ( configJson is null )
                    {
                        throw new Exception($"Trouble deserializing the file at '{ClientConfigFilePath}'");
                    }

                    configJson.ServerAddress = p_serverAddress;
                    configJson.Port          = p_port;

                    await File.WriteAllTextAsync(ClientConfigFilePath, JsonSerializer.Serialize(configJson));
                }
            }
            catch
            {
                // Do Nothing, because if we can't store the valid server than we can't store it. - Comment by Jamie McCoard on 02/12/2022 @ 14:57:08
            }
        }
    }
}
