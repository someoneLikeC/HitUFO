using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    CCPlayDiskAction PlayDiskAction; // �ɵ����ж���

    public void PlayDisk(GameObject disk, float speed, Vector3 direction)
    {
        PlayDiskAction = CCPlayDiskAction.GetSSAction(direction, speed);
        RunAction(disk, PlayDiskAction, this);
    }

    // �ص�����
    public void SSActionEvent(SSAction source,
    SSActionEventType events = SSActionEventType.Competed,
    int intParam = 0,
    string strParam = null,
    Object objectParam = null)
    {
        // �������к���շɵ�
        Singleton<RoundController>.Instance.FreeFactoryDisk(source.gameObject);
    }
}

