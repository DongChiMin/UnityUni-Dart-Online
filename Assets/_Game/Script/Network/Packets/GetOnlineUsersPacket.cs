[System.Serializable]
public class GetOnlineUsersPacket : BasePacket
{
    public GetOnlineUsersPacket()
    {
        action = "get_online_users";
    }
}
