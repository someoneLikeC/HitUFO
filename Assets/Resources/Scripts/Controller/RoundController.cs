using System.Data;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    private IActionManager actionManager; // ѡ��ɵ����˶�����
    private ScoreRecorder scoreRecorder; // �Ƿ���
    private FirstController firstController;
    private Ruler ruler; // �ɵ���ȡ����

    void Start()
    {
        // һ��ʼ�ɵ����˶�����Ĭ��Ϊ�˶�ѧ�˶�
        actionManager = gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<PhysisActionManager>();
        scoreRecorder = new ScoreRecorder();
        firstController = SSDirector.GetInstance().currentController;
        gameObject.AddComponent<DiskFactory>();
        InitRuler();
    }

    void InitRuler()
    {
        ruler.trialNum = 0;
        ruler.roundNum = 0;
        ruler.sendTime = 0;
        ruler.roundDisksNum = new int[10];
        generateRoundDisksNum();
    }

    // ����ÿtrialͬʱ�����ķɵ����������飬ͬʱ�����ɵ�����������4
    public void generateRoundDisksNum()
    {
        for (int i = 0; i < 10; ++i)
        {
            ruler.roundDisksNum[i] = Random.Range(0, 4) + 1;
        }
    }

    public void Reset()
    {
        InitRuler();
        scoreRecorder.Reset();
    }

    public void Record(DiskData disk)
    {
        scoreRecorder.Record(disk);
    }

    public int GetScores()
    {
        return scoreRecorder.score;
    }

    public void SetRoundSum(int roundSum)
    {
        ruler.roundSum = roundSum;
    }

    // ������Ϸģʽ��ͬʱ֧�������˶�ģʽ�Ͷ���ѧ�˶�ģʽ
    public void SetPlayDiskModeToPhysis(bool isPhysis)
    {
        if (isPhysis)
        {
            actionManager = Singleton<PhysisActionManager>.Instance as IActionManager;
        }
        else
        {
            actionManager = Singleton<CCActionManager>.Instance as IActionManager;
        }
    }

    // ����ɵ�
    public void LaunchDisk()
    {
        // ʹ�ɵ�����λ�þ����ֿܷ����Ӳ�ͬλ�÷���ʹ�õ�����
        int[] beginPosY = new int[4] { 0, 0, 0, 0 };

        for (int i = 0; i < ruler.roundDisksNum[ruler.trialNum]; ++i)
        {
            // ��ȡ�����
            int randomNum = Random.Range(0, 3) + 1;
            // �ɵ��ٶ���غ������Ӷ���죬�����Ѷ�����
            ruler.speed = randomNum * (ruler.roundNum + 4);

            // ����ѡȡ������������������ѡ��ɵ���ɫ
            randomNum = Random.Range(0, 3) + 1;
            if (randomNum == 1)
            {
                ruler.color = "red";
            }
            else if (randomNum == 2)
            {
                ruler.color = "green";
            }
            else
            {
                ruler.color = "blue";
            }

            // ����ѡȡ������������������ѡ��ɵ��Ĵ�С
            ruler.size = Random.Range(0, 3) + 1;

            // ����ѡȡ������������������ѡ��ɵ�����ķ���
            randomNum = Random.Range(0, 2);
            if (randomNum == 1)
            {
                ruler.direction = new Vector3(3, 1, 0);
            }
            else
            {
                ruler.direction = new Vector3(-3, 1, 0);
            }

            // ����ѡȡ���������ʹ��ͬ�ɵ��ķ���λ�þ����ֿܷ�
            do
            {
                randomNum = Random.Range(0, 4);
            } while (beginPosY[randomNum] != 0);
            beginPosY[randomNum] = 1;
            ruler.beginPos = new Vector3(-ruler.direction.x * 4, -0.5f * randomNum, 0);

            // ����ruler�ӹ���������һ���ɵ�
            GameObject disk = Singleton<DiskFactory>.Instance.GetDisk(ruler);

            // ���÷ɵ��ķ��ж���
            actionManager.PlayDisk(disk, ruler.speed, ruler.direction);
        }
    }

    // �ͷŹ����ɵ�
    public void FreeFactoryDisk(GameObject disk)
    {
        Singleton<DiskFactory>.Instance.FreeDisk(disk);
    }

    // �ͷ����й����ɵ�
    public void FreeAllFactoryDisk()
    {
        GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject g in obj)
        {
            if (g.gameObject.name == "Disk(Clone)(Clone)")
            {
                Singleton<DiskFactory>.Instance.FreeDisk(g);
            }
        }
    }

    void Update()
    {
        if (firstController.GetGameState() == (int)GameState.Playing)
        {
            ruler.sendTime += Time.deltaTime;
            // ÿ��2s����һ�ηɵ�(trial)
            if (ruler.sendTime > 2)
            {
                ruler.sendTime = 0;
                // ���Ϊ���޻غϻ�δ���趨�غ���
                if (ruler.roundSum == -1 || ruler.roundNum < ruler.roundSum)
                {
                    // ����ɵ�������trial����
                    firstController.SetViewTip("");
                    LaunchDisk();
                    ruler.trialNum++;
                    // ������trial����10ʱ��˵��һ���غ��Ѿ��������غϼ�һ���������ɷɵ�����
                    if (ruler.trialNum == 10)
                    {
                        ruler.trialNum = 0;
                        ruler.roundNum++;
                        generateRoundDisksNum();
                    }
                }
                // ������Ϸ��������ʾ���½�����Ϸ
                else
                {
                    firstController.SetViewTip("Click Restart and Play Again!");
                    firstController.SetGameState((int)GameState.GameOver);
                }
                // ���ûغ�����trial��Ŀ����ʾ
                if (ruler.trialNum == 0) firstController.SetViewRoundNum(ruler.roundNum);
                else firstController.SetViewRoundNum(ruler.roundNum + 1);
                firstController.SetViewTrialNum(ruler.trialNum);
            }
        }
    }
}
