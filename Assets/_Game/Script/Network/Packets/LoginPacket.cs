[System.Serializable]
public class LoginData
{
    public string username;
    public string password;
}

[System.Serializable]
public class LoginPacket : BasePacket
{
    public LoginData data;

    public LoginPacket(string username, string password)
    {
        action = "login";
        data = new LoginData
        {
            username = username,
            password = password
        };
    }
}
