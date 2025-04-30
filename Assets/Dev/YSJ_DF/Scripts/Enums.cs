namespace Scripts
{
    public enum SceneID
    {
        None,           // �⺻ ���� 
        Title,          // Title 1
        CharacterChoose,// InGame 2
        InGame,         // InGame 2
    }

    // ��� Ȯ��
    public enum GameResultState
    {
        None,   // ���� ��� ����
        Clear,  // Ŭ���� ����
        Fail    // Ŭ���� ����
    }

    // ���� ���¿�
    public enum GamePlayState
    {
        None,     // �⺻ ���� (���û���)
        Playing,  // ������ ���� ��
        Paused,   // �Ͻ� ����
        Stopped   // ���� ����
    }
}