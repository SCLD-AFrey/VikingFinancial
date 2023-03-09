using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TransactionClientGui.Services.Infrastructure;

public class CommonDirectories
{
    private readonly ILogger<CommonDirectories> m_logger;
    
    public CommonDirectories(ILogger<CommonDirectories> p_logger)
    {
        m_logger = p_logger;
        Console.WriteLine("CommonDirectories");
        CreateFolders();
    }

    public string ServerDataPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".VikingTransactionClient");

    
    private void CreateFolders()
    {
        var newDirectory = Directory.CreateDirectory(ServerDataPath);
    }
}