[System.Serializable]
public class GetPlayerDetailData
{
    public string username;
}

[System.Serializable]
public class GetPlayerDetailPacket : BasePacket
{
    public GetPlayerDetailData data;

    public GetPlayerDetailPacket(string username)
    {
        action = "get_player_detail";
        data = new GetPlayerDetailData
        {
            username = username
        };
    }
}
