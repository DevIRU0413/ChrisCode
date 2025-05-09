namespace Scripts
{
    public enum SceneID
    {
        None,           // 기본 상태 
        Title,          // Title 1
        CharacterChoose,// InGame 2
        InGame,         // InGame 2
    }

    // 결과 확인
    public enum GameResultState
    {
        None,   // 아직 결과 없음
        Clear,  // 클리어 성공
        Fail    // 클리어 실패
    }

    // 게임 상태용
    public enum GamePlayState
    {
        None,     // 기본 상태 (선택사항)
        Playing,  // 게임이 진행 중
        Paused,   // 일시 정지
        Stopped   // 강제 멈춤
    }

    // 게임 상태용
    public enum ManagerPriority
    {
        None,                   // Error
        ResourceManager = 100, // Manager
        SceneManagerEx,
        AudioManager,
        UIManager,
        InGameManager,
        MonsterManager,
    }
}