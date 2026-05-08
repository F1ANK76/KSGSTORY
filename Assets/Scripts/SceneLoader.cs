using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button _playBtn; // Play 버튼
    public TMP_InputField _nicknameInput; // TMP Input Field

    private void Start()
    {
        if (_playBtn != null)
            _playBtn.onClick.AddListener(OnClickPlay);
    }

    public void OnClickPlay()
    {
        // 입력값 저장 (씬 전환 후에도 사용 가능하게)
        string nickname = _nicknameInput.text;
        PlayerPrefs.SetString("PlayerNickname", nickname);

        // 씬 전환
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickBoss()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.CmdGoBoss();
        }
    }
}