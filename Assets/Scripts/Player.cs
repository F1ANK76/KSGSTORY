using Mirror;
using UnityEngine;
using TMPro;

public class Player : NetworkBehaviour
{
    public float speed = 5f;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnSwordScaleYChanged))]
    private float _swordScaleY = 5f;

    [SyncVar(hook = nameof(OnSwordScaleXChanged))]
    private float _swordScaleX = 0.35f;

    public TextMeshProUGUI _nameTMP;

    [SerializeField] private Transform _sword;

    private UIManager _uiManager;

    public override void OnStartLocalPlayer()
    {
        string name = PlayerPrefs.GetString("PlayerNickname", "Player");
        CmdSetName(name);

        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetTarget(transform);

        _uiManager = FindFirstObjectByType<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.SetPlayer(this);
        }
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
    public void CmdSetSwordScaleX(float value)
    {
        _swordScaleX = value;
    }

    private void OnSwordScaleXChanged(float oldValue, float newValue)
    {
        Vector3 scale = _sword.localScale;
        scale.x = newValue;
        _sword.localScale = scale;
    }

    [Command]
    public void CmdSetSwordScaleY(float value)
    {
        _swordScaleY = value;
    }

    private void OnSwordScaleYChanged(float oldValue, float newValue)
    {
        Vector3 scale = _sword.localScale;
        scale.y = newValue;
        _sword.localScale = scale;
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