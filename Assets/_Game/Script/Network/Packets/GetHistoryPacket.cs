[System.Serializable]
public class GetHistoryPacket : BasePacket
{
    public GetHistoryPacket()
    {
        action = "get_history";
    }
}
