using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using TMPro;

public class NicknameGenerator : MonoBehaviour
{
    private string _apiKey = "AIzaSyAF8QpJCjzI1ZzdW3rqq0RbNopkOVDBrho";

    public TextMeshProUGUI _nicknameTMP;
    public Button _generateBtn;

    void Start()
    {
        if (_generateBtn != null)
        {
            _generateBtn.onClick.AddListener(OnClickGenerate);
        }
    }

    public void OnClickGenerate()
    {
        StartCoroutine(RequestGemini());
    }

    IEnumerator RequestGemini()
    {
        string url = "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key=" + _apiKey;

        string json = @"
        {
            ""contents"": [
                {
                    ""parts"": [
                        { ""text"": ""RPG 게임에 어울리는 닉네임 5개 만들어줘. 단어만 줄바꿈으로 깔끔하게 출력해줘"" }
                    ]
                }
            ]
        }";

        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonText = www.downloadHandler.text;

            GeminiResponse response = JsonUtility.FromJson<GeminiResponse>(jsonText);

            string result = response.candidates[0].content.parts[0].text;

            result = result.Replace(",", "\n");

            _nicknameTMP.text = result;
        }
        else
        {
            Debug.LogError(www.error);
            Debug.LogError(www.downloadHandler.text);
        }
    }

    [System.Serializable]
    public class GeminiResponse
    {
        public Candidate[] candidates;
    }

    [System.Serializable]
    public class Candidate
    {
        public Content content;
    }

    [System.Serializable]
    public class Content
    {
        public Part[] parts;
    }

    [System.Serializable]
    public class Part
    {
        public string text;
    }
}