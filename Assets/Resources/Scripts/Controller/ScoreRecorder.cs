public class ScoreRecorder
{
    public int score; // ��Ϸ����

    public ScoreRecorder()
    {
        score = 0;
    }

    /* ��¼���������ݵ���еķɵ��Ĵ�С���ٶȣ���ɫ����÷� */
    public void Record(DiskData disk)
    {
        // �ɵ�ԽС�־�Խ�ߣ���СΪ1��3�֣���СΪ2��2�֣���СΪ3��1��
        int diskSize = disk.size;
        switch (diskSize)
        {
            case 1:
                score += 3;
                break;
            case 2:
                score += 2;
                break;
            case 3:
                score += 1;
                break;
            default: break;
        }

        // �ٶ�Խ��־�Խ��
        score += disk.speed;

        // ��ɫΪ��ɫ��1�֣���ɫΪ��ɫ��2�֣���ɫΪ��ɫ��3��
        string diskColor = disk.color;
        if (diskColor.CompareTo("red") == 0)
        {
            score += 1;
        }
        else if (diskColor.CompareTo("green") == 0)
        {
            score += 2;
        }
        else if (diskColor.CompareTo("blue") == 0)
        {
            score += 3;
        }
    }

    /* ���÷�������Ϊ0 */
    public void Reset()
    {
        score = 0;
    }
}
