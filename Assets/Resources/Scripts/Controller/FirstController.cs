/* ��Ϸ״̬��0Ϊ׼�����У�1Ϊ���ڽ�����Ϸ��2Ϊ���� */
using UnityEngine;

enum GameState
{
    Ready = 0, Playing = 1, GameOver = 2
};

public class FirstController : MonoBehaviour
{
    private RoundController roundController; // �غϿ�����
    private View view; // ��Ϸ��ͼ
    private int N; // Ĭ����Ϸ�غ�
    private int gameState;
    public GUISkin gameSkin;

    void Awake()
    {
        SSDirector.GetInstance().currentController = this;
        roundController = gameObject.AddComponent<RoundController>();
        view = gameObject.AddComponent<View>();
        gameState = (int)GameState.Ready;
        view.gameSkin = gameSkin;
        N = 2;
    }

    public int GetN()
    {
        return N;
    }

    public void Restart()
    {
        view.Init();
        roundController.Reset();
    }

    public void SetGameState(int state)
    {
        gameState = state;
    }

    public int GetGameState()
    {
        return gameState;
    }

    public void ShowPage()
    {
        switch (gameState)
        {
            case 0:
                view.ShowHomePage();
                break;
            case 1:
                view.ShowGamePage();
                break;
            case 2:
                view.ShowRestart();
                break;
        }
    }

    public void SetRoundSum(int roundSum)
    {
        roundController.SetRoundSum(roundSum);
    }

    public void SetPlayDiskModeToPhysis(bool isPhysis)
    {
        roundController.SetPlayDiskModeToPhysis(isPhysis);
    }

    public void SetViewTip(string tip)
    {
        view.SetTip(tip);
    }

    public void SetViewScore(int score)
    {
        view.SetScore(score);
    }

    public void SetViewRoundNum(int round)
    {
        view.SetRoundNum(round);
    }

    public void SetViewTrialNum(int trial)
    {
        view.SetTrialNum(trial);
    }

    public void Hit(Vector3 position)
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.parent.GetComponent<DiskData>() != null)
            {
                // �ѻ��еķɵ��Ƴ���Ļ�������ص��ͷ�
                hit.collider.transform.parent.transform.position = new Vector3(0, -6, 0);
                // ��¼�ɵ��÷�
                roundController.Record(hit.collider.transform.parent.GetComponent<DiskData>());
                // ��ʾ��ǰ�÷�
                view.SetScore(roundController.GetScores());
            }
        }
    }

    // �ͷ����й����ɵ�
    public void FreeAllFactoryDisk()
    {
        roundController.FreeAllFactoryDisk();
    }
}
