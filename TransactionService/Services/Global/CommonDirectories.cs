namespace TransactionService.Services.Global;

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
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".VikingTransactionService");

    
    private void CreateFolders()
    {
        var newDirectory = Directory.CreateDirectory(ServerDataPath);
    }

}