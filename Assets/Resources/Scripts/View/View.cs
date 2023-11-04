using UnityEngine;

public class View : MonoBehaviour
{
    private FirstController mainController;
    private int score;
    private string tip;
    private string roundNum;
    private string trialNum;
    public GUISkin gameSkin;  // ��Ϸ�ؼ���Ƥ�����

    void Start()
    {
        score = 0;
        tip = "";
        roundNum = "";
        trialNum = "";
        mainController = SSDirector.GetInstance().currentController;
    }

    public void SetTip(string tip)
    {
        this.tip = tip;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetRoundNum(int round)
    {
        roundNum = "�غ�: " + round;
    }

    public void SetTrialNum(int trial)
    {
        if (trial == 0) trial = 10;
        trialNum = "Trial: " + trial;
    }

    public void Init()
    {
        score = 0;
        tip = "";
        roundNum = "";
        trialNum = "";
    }

    public void AddTitle()
    {
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.normal.textColor = Color.black;
        titleStyle.fontSize = 50;

        GUI.Label(new Rect(Screen.width / 2 - 80 , 20, 60, 100), "��ɵ�", titleStyle);
    }

    public void AddChooseModeButton()
    {
        GUI.skin = gameSkin;
        if (GUI.Button(new Rect(Screen.width / 2 - 80 , 100, 160, 80), "��ͨģʽ\n(Ĭ��Ϊ" + mainController.GetN() + "�غ�)"))
        {
            mainController.SetRoundSum(mainController.GetN());
            mainController.Restart();
            mainController.SetGameState((int)GameState.Playing);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 80 , 210, 160, 80), "�޾�ģʽ\n(�غ�������)"))
        {
            mainController.SetRoundSum(-1);
            mainController.Restart();
            mainController.SetGameState((int)GameState.Playing);
        }
    }

    public void ShowHomePage()
    {
        AddChooseModeButton();
    }

    public void AddActionModeButton()
    {
        GUI.skin = gameSkin;
        if (GUI.Button(new Rect(10, Screen.height - 100, 110, 40), "�˶�ѧģʽ"))
        {
            mainController.FreeAllFactoryDisk();
            mainController.SetPlayDiskModeToPhysis(false);
        }
        if (GUI.Button(new Rect(10, Screen.height - 50, 110, 40), "����ģʽ(�ɵ�ֱ�ӻ�����ײ)"))
        {
            mainController.FreeAllFactoryDisk();
            mainController.SetPlayDiskModeToPhysis(true);
        }
    }

    public void AddBackButton()
    {
        GUI.skin = gameSkin;
        if (GUI.Button(new Rect(10, 10, 60, 40), "Back"))
        {
            mainController.FreeAllFactoryDisk();
            mainController.Restart();
            mainController.SetGameState((int)GameState.Ready);
        }
    }

    public void AddGameLabel()
    {
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.black;
        labelStyle.fontSize = 30;

        GUI.Label(new Rect(Screen.width - 160, 10, 100, 50), "�÷�: " + score, labelStyle);
        GUI.Label(new Rect(Screen.width / 2 - 200, 80, 50, 200), tip, labelStyle);
        GUI.Label(new Rect(Screen.width - 160, 60, 100, 50), roundNum, labelStyle);
        GUI.Label(new Rect(Screen.width - 160, 110, 100, 50), trialNum, labelStyle);
    }

    public void AddRestartButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 80 , 150, 100, 60), "Restart"))
        {
            mainController.FreeAllFactoryDisk();
            mainController.Restart();
            mainController.SetGameState((int)GameState.Playing);
        }
    }

    public void ShowGamePage()
    {
        AddGameLabel();
        AddBackButton();
        AddActionModeButton();
        if (Input.GetButtonDown("Fire1"))
        {
            mainController.Hit(Input.mousePosition);
        }
    }

    public void ShowRestart()
    {
        ShowGamePage();
        AddRestartButton();
    }

    void OnGUI()
    {
        AddTitle();
        mainController.ShowPage();
    }
}
