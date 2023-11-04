using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    // ��������
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    // ������ʼ�Ķ����ĵȴ��������
    private List<SSAction> waitingAdd = new List<SSAction>();
    // ����ɵĵĶ����ĵȴ�ɾ������
    private List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        // ���뼴����ʼ�Ķ���
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        // ��յȴ��������
        waitingAdd.Clear();

        // �������붯��
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        // �������ɵĶ���
        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        // ��յȴ�ɾ������
        waitingDelete.Clear();
    }

    // ��ʼ�����������뵽�ȴ��������
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }
}
