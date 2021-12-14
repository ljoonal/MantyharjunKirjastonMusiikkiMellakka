using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BackendHandler : MonoBehaviour
{
    const string backendHighscoresURL = "http://172.30.139.144/musamellakka/highscores.php";

    HighScores.HighScores hs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequestForHighScoresFile(backendHighscoresURL));
    }

    IEnumerator GetRequestForHighScoresFile(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // set downloadHandler for json
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            // Request and wait for reply
            yield return webRequest.SendWebRequest();
            // get raw data and convert it to string
            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
            if (webRequest.result != UnityWebRequest.Result.ConnectionError) 
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
        }
        CreateHighscoreList();
    }

    public void SendDataToDB(string name, int scrore)
    {
        HighScores.HighScore newHs = new HighScores.HighScore();
        newHs.playername = name;
        newHs.score = scrore;

        StartCoroutine(PostRequestForHighScoresFile());

        IEnumerator PostRequestForHighScoresFile()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(backendHighscoresURL, JsonUtility.ToJson(newHs)))
            {
                // set downloadHandler for json
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("Accept", "application/json");
                // Request and wait for reply
                yield return webRequest.SendWebRequest();
                // get raw data and convert it to string
                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
            }

        }
    }

    void CreateHighscoreList()
    {
        string positions = "";
        string nameList = "";
        string scoreList = "";

        if(hs != null)
        {
            for(int i = 0; i < hs.scores.Length; i++)
            {
                positions += (i + 1).ToString() + ".\n";
                nameList += hs.scores[i].playername + "\n";
                scoreList += hs.scores[i].score + "\n"; 
            }
        }
        else scoreList = "Error occured";
        GameObject.Find("Positions").GetComponent<Text>().text = positions;
        GameObject.Find("PlayerNames").GetComponent<Text>().text = nameList;
        GameObject.Find("PlayerScores").GetComponent<Text>().text = scoreList;
    }
}
