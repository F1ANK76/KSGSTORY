using Mirror;
using UnityEngine;
using TMPro;

public class Player : NetworkBehaviour
{
    public float speed = 5f;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    public TextMeshProUGUI _nameTMP;

    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private Transform _attackPoint;

    private UIManager _uiManager;

    public override void OnStartLocalPlayer()
    {
        string name = PlayerPrefs.GetString("PlayerNickname", "Player");
        CmdSetName(name);

        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetTarget(transform);

        _uiManager = FindFirstObjectByType<UIManager>();
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_uiManager != null && _uiManager.IsSkillUnlocked(0))
            {
                CmdSlash();
            }
        }
    }

    [Command]
    private void CmdSlash()
    {
        bool isRight = transform.localScale.x > 0;

        GameObject slash = Instantiate(_slashPrefab, _attackPoint.position, Quaternion.identity);

        Slash s = slash.GetComponent<Slash>();
        s.Init(isRight);

        NetworkServer.Spawn(slash);

        Destroy(slash, 0.3f);
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