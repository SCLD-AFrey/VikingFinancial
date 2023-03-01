namespace TransactionClient.Models.DataStructures;

public class User
{
    public string    Username                          { get; private init; } = string.Empty;
    public string    AuthorizationToken                { get; private init; } = string.Empty;

    public static User GenerateUserFromServerResponse(string p_userName, string p_token)
    {
        return new User
        {
            Username           = p_userName,
            AuthorizationToken = p_token
        };
    }
}