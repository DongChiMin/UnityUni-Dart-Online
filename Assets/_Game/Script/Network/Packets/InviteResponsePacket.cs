[System.Serializable]
public class InviteResponseData
{
    public string inviter;
    public string response;  // "ACCEPT" hoặc "DECLINE"
}

[System.Serializable]
public class InviteResponsePacket : BasePacket
{
    public InviteResponseData data;

    public InviteResponsePacket(string inviter, string response)
    {
        action = "invite_response";
        data = new InviteResponseData
        {
            inviter = inviter,
            response = response
        };
    }
}
