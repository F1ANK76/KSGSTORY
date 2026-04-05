using Mirror;
using UnityEngine;
using TMPro;

public class Player : NetworkBehaviour
{
    public float speed = 5f;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    public TextMeshProUGUI _nameTMP;

    public override void OnStartLocalPlayer()
    {
        string name = PlayerPrefs.GetString("PlayerNickname", "Player");
        CmdSetName(name);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    [Command]
    private void CmdSetName(string name)
    {
        playerName = name;
    }

    private void OnNameChanged(string oldName, string newName)
    {
        if (_nameTMP != null)
        {
            _nameTMP.text = newName;
        }
    }
}