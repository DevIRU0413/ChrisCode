using Scripts.Interface;
using UnityEngine;
using UnityEngine.Events;

// 게임의 승, 패 판단
// 게임 실행 시간 판단
// 플레이어블 캐릭터 상태 추적
namespace Scripts.Manager
{

    public class InGameManager : SimpleSingleton<InGameManager>, IManager
    {
        private bool _isFirstFrameCheck = false;

        [SerializeField] private GamePlayState _playState;
        [SerializeField] private GameResultState _resultState;

        [SerializeField] private GameObject _playerObject;
        private PlayerHp _playerHpCmp;

        [SerializeField] private float _inGameClearMinutes = 5.0f; // 클리어 시간 (10.5m => 630s)
        private float _inGameClearTime;
        private float _inGameStartTime; // 시작 시간
        private float _inGameCurrenttTime; // 현재 시간

        // 외부 연결 가능 이벤트들(최소 Start에서는 넣어주기)
        public UnityEvent OnPlayStateEntered;
        public UnityEvent OnPauseStateEntered;
        public UnityEvent OnStopStateEntered;

        public int Priority => (int)ManagerPriority.InGameManager;

        // IManager
        public void Initialize()
        {
            InitializePlayer();
            InitializeTime();

            InitializeState();
        }
        public void Cleanup()
        {

        }
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        // Unity
        public void Update()
        {
            // check
            CheckFirstFrame();
            CheckResultState();

            // update
            UpdateTime();
        }

        // Init
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

        // Update
        private void UpdateTime()
        {
            _inGameCurrenttTime += Time.deltaTime;
        }

        // Check
        private void CheckFirstFrame()
        {
            if (_isFirstFrameCheck) return;

            _isFirstFrameCheck = true;
            OnPlayStateEntered.Invoke();
        }
        private void CheckResultState()
        {
            // 게임 결과가 나왔을 때
            if (_resultState != GameResultState.None || _playerHpCmp == null)
                return;

            // Checking
            if (_playerHpCmp.CurrentHealth <= 0)
                _resultState = GameResultState.Fail;
            else if (_inGameClearTime <= _inGameCurrenttTime)
                _resultState = GameResultState.Clear;
        }


        // UI 쪽에서 버튼 같은거 눌렀을 때 전환 눌러주면 됨.
        // UnityEvent에 팝업 같은거 보여주는 식으로 사용가능
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

        // UI 쪽에서 가져다가 사용하면 됩니다.
        public GameResultState GetGameResultState()
        {
            return _resultState;
        }
        public float GetInGameCurrenttTime()
        {
            return _inGameCurrenttTime;
        }

    }
}