namespace Scripts
{
    public enum SceneType
    {
        None = 0,
        Title,
        InGame,
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
        Stopped   // ���� ���� (���� ���)
    }
}