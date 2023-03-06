namespace TransactionClient.Models;

public class ServerCredentials
{
#pragma warning disable CS8618
    public ServerCredentials()
#pragma warning restore CS8618
    {
    }
    public ServerCredentials(
        string p_serverAddress, 
        ushort p_serverPort)
    {
        ServerAddress = p_serverAddress;
        ServerPort = p_serverPort;
        UserName = "";
        Password = "";
    }

    public string ServerAddress { get; set; }
    public ushort ServerPort { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}