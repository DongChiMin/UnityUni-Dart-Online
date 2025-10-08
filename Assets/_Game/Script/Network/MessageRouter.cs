using System;
using UnityEngine;

public class MessageRouter : Singleton<MessageRouter>
{
    private LoginHandler loginHandler = new LoginHandler();

    public void Route(string msg)
    {
        ResponseData responseData = JsonUtility.FromJson<ResponseData>(msg);
        string response = responseData.response;

        switch (response)
        {
            case "response_login":
                loginHandler.Handle(responseData);
                break;
            case "response_register":
                break;

            case "response_online_users":
                break;
            case "response_invite":
                break;
            case "response_invite_response":
                break;
            case "response_match_start":
                break;
            case "response_throw_dart":
                break;
            case "response_round_result":
                break;
            case "response_match_detail":
                break;
            case "response_exit_match":
                break;
            case "response_history":
                break;
            case "response_ranking":
                break;
            case "response_player_detail":
                break;

            default:
                Debug.LogWarning("Không biết xử lý: " + msg);
                break;
        }
    }
}
