/* 游戏状态，0为准备进行，1为正在进行游戏，2为结束 */
using UnityEngine;

enum GameState
{
    Ready = 0, Playing = 1, GameOver = 2
};

public class FirstController : MonoBehaviour
{
    private RoundController roundController; // 回合控制器
    private View view; // 游戏视图
    private int N; // 默认游戏回合
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
                // 把击中的飞碟移出屏幕，触发回调释放
                hit.collider.transform.parent.transform.position = new Vector3(0, -6, 0);
                // 记录飞碟得分
                roundController.Record(hit.collider.transform.parent.GetComponent<DiskData>());
                // 显示当前得分
                view.SetScore(roundController.GetScores());
            }
        }
    }

    // 释放所有工厂飞碟
    public void FreeAllFactoryDisk()
    {
        roundController.FreeAllFactoryDisk();
    }
}
