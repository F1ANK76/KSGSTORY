using Mirror;
using UnityEngine;
using TMPro;

public class Player : NetworkBehaviour
{
    public float speed = 5f;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    public TextMeshProUGUI _nameTMP; // 머리 위 TMP

    private void Start()
    {
        // 로컬 플레이어라면 PlayerPrefs에서 닉네임 가져오기
        if (isLocalPlayer)
        {
            playerName = PlayerPrefs.GetString("PlayerNickname", "Player");
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    // SyncVar hook으로 다른 클라이언트에도 적용
    void OnNameChanged(string oldName, string newName)
    {
        if (_nameTMP != null)
            _nameTMP.text = newName;
    }
}