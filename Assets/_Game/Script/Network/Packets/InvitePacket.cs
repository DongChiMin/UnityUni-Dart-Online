[System.Serializable]
public class InviteData
{
    public string targetUsername;
}

[System.Serializable]
public class InvitePacket : BasePacket
{
    public InviteData data;

    public InvitePacket(string targetUsername)
    {
        action = "invite";
        data = new InviteData
        {
            targetUsername = targetUsername
        };
    }
}
