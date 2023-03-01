namespace TransactionClient.Models.DataStructures;

public class ServerLoginCredentials
{
    public ServerLoginCredentials(string p_serverAddress, ushort p_serverPort, string p_userName, string p_password)
    {
        ServerAddress = p_serverAddress;
        ServerPort    = p_serverPort;
        Username      = p_userName;
        Password      = p_password;
    }
    
    public string ServerAddress { get; set; }
    public ushort ServerPort    { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}