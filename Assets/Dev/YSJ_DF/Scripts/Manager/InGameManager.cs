using Scripts;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

// ������ ��, �� �Ǵ�
// ���� ���� �ð� �Ǵ�
// �÷��̾�� ĳ���� ���� ����
public class InGameManager : MonoBehaviour
{
    private bool _isFirstFrameCheck = false;

    [SerializeField] private GamePlayState _playState;
    [SerializeField] private GameResultState _resultState;

    [SerializeField] private GameObject _playerObject;
    private PlayerHp _playerHpCmp;

    [SerializeField] private float _inGameClearMinutes = 5.0f; // Ŭ���� �ð� (10.5m => 630s)
    private float _inGameClearTime;
    private float _inGameStartTime; // ���� �ð�
    private float _inGameCurrenttTime; // ���� �ð�

    // �ܺ� ���� ���� �̺�Ʈ��(�ּ� Start������ �־��ֱ�)
    public UnityEvent OnPlayStateEntered;
    public UnityEvent OnPauseStateEntered;
    public UnityEvent OnStopStateEntered;

    private void Awake()
    {
        Debug.Log(GameManager.Instance);
    }

    private void Start()
    {
        InitializePlayer();
        InitializeTime();

        InitializeState();
    }

    private void Update()
    {
        // check
        CheckFirstFrame();
        CheckResultState();

        // update
        UpdateTime();
    }

    private void InitializePlayer()
    {
        if (_playerObject == null)
            _playerObject = GameObject.FindGameObjectWithTag("Player");

        if (_playerObject != null)
            _playerHpCmp = _playerObject.GetComponent<PlayerHp>();
    }
    private void InitializeTime()
    {
        _inGameClearTime = _inGameClearMinutes * 60.0f;
        _inGameStartTime = 0;
        _inGameCurrenttTime = _inGameStartTime;
    }
    private void InitializeState()
    {
        _resultState = GameResultState.None;
        _playState = GamePlayState.Playing;
    }

    private void UpdateTime()
    {
        _inGameCurrenttTime += Time.deltaTime;
    }

    private void CheckFirstFrame()
    {
        if (_isFirstFrameCheck) return;

        _isFirstFrameCheck = true;
        OnPlayStateEntered.Invoke();
    }
    private void CheckResultState()
    {
        // ���� ����� ������ ��
        if (_resultState != GameResultState.None || _playerHpCmp == null)
            return;

        // Checking
        if (_playerHpCmp.CurrentHealth <= 0)
            _resultState = GameResultState.Fail;
        else if (_inGameClearTime <= _inGameCurrenttTime)
            _resultState = GameResultState.Clear;
    }

    public void SetPlayState(GamePlayState state)
    {
        if (_playState == state) return;
        _playState = state;

        switch (state)
        {
            case GamePlayState.Playing:
                OnPlayStateEntered?.Invoke();
                break;
            case GamePlayState.Paused:
                OnPauseStateEntered?.Invoke();
                break;
            case GamePlayState.Stopped:
                OnStopStateEntered?.Invoke();
                break;
        }
    }
}
