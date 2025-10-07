[System.Serializable]
public class RegisterData
{
    public string username;
    public string password;
    public string playerName;
}

[System.Serializable]
public class RegisterPacket : BasePacket
{
    public RegisterData data;

    public RegisterPacket(string username, string password, string playerName)
    {
        action = "register";
        data = new RegisterData
        {
            username = username,
            password = password,
            playerName = playerName
        };
    }
}
