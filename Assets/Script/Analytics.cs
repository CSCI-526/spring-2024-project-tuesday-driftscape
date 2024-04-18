using UnityEngine;
using System.Collections;
using Proyecto26;
using UnityEngine.Networking;
using System;

public class LevelCompleteAnalytics : MonoBehaviour
{
    public void SendLevelCompleteEvent(string levelName, bool success, float timeElapsed, int flytimes, int faketimes, int health, float locationX, float locationY)
    {
        Debug.Log(levelName);
        Debug.Log(success);
        Debug.Log(timeElapsed);
        StartCoroutine(SendLevelCompleteEventCoroutine(levelName, success, timeElapsed, flytimes, faketimes, health, locationX, locationY));
    }   

    private IEnumerator SendLevelCompleteEventCoroutine(string levelName, bool success, float timeElapsed, int flytimes, int faketimes, int health, float locationX, float locationY)
    {
        Debug.Log(levelName);
        Debug.Log(success);
        Debug.Log(timeElapsed);
        PlayerData player = new PlayerData();
        player.levelName = levelName;
        player.success = success;
        player.timeElapsed = timeElapsed;
        player.flytimes = flytimes;
        player.faketimes = faketimes;
        player.health = health;
        player.locationX = locationX;
        player.locationY = locationY;
        string json = JsonUtility.ToJson(player);

        RestClient.Post("https://driftspace-default-rtdb.firebaseio.com/.json", json).Then(response =>
        {
            Debug.Log("Data sent successfully!");
        }).Catch(error =>
        {
            Debug.LogError("Failed to send data: " + error.Message);
        });

        yield return null;
    }
}

[System.Serializable]
public class PlayerData
{
    public string levelName;
    public bool success;
    public float timeElapsed;
    public float locationX = 0;
    public float locationY = 0;
    public int flytimes = 0;
    public int faketimes = 0;
    public int health = 100;

}
