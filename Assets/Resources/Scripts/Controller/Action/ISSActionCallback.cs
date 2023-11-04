using UnityEngine;

public enum SSActionEventType : int { Started, Competed }
public interface ISSActionCallback
{
    // �ص�����
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null);
}
