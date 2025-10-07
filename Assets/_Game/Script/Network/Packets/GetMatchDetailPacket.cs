[System.Serializable]
public class GetMatchDetailData
{
    public int matchId;
}

[System.Serializable]
public class GetMatchDetailPacket : BasePacket
{
    public GetMatchDetailData data;

    public GetMatchDetailPacket(int matchId)
    {
        action = "get_match_detail";
        data = new GetMatchDetailData
        {
            matchId = matchId
        };
    }
}
