[System.Serializable]
public class LogoutPacket : BasePacket
{
    public LogoutPacket()
    {
        action = "logout";
    }
}
