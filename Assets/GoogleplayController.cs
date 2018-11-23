using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GoogleplayController : MonoBehaviour {

    bool success;
    // Use this for initialization
   public GameObject connectedMenu;
   public GameObject disconnectedMenu;
    
	void Start () {



        PlayGamesPlatform.Activate();
        OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);

    }
	
	// Update is called once per frame
	void Update () {
	}


    public void OnConnectClick()
    {
        Social.localUser.Authenticate((bool success) => { OnConnectionResponse(success); });
    }

    private void OnConnectionResponse(bool authenticated)
    {
        if(authenticated)
        {
            connectedMenu.SetActive(true);
            disconnectedMenu.SetActive(false);
         
        }
        else
        {   
            connectedMenu.SetActive(false);
            disconnectedMenu.SetActive(true);
           
        }

    }
    public void ShowAchievementClick()
    {
        if(Social.localUser.authenticated)   // om inloggad
        {
            Social.ShowAchievementsUI();
        }
    }
    public void OnLeaderBoardcheck()
    {

        if (Social.localUser.authenticated)   // om inloggad
        {
            Social.ShowLeaderboardUI();
        }
    }
    public void UnlockAchievement(string AchievementID)
    {
        Social.ReportProgress(AchievementID,100.0f,(bool success) =>
        {
            Debug.Log("Achievement unlockedoes it work"+ success.ToString());
        });
    }
    public void Reportscore(int score)
    {
        Social.ReportScore(score, GooglePlayAchievement.leaderboard_highscore, (bool success) => {

            Debug.Log("successs score" + score.ToString());
        });

    }
}

