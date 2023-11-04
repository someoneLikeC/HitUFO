using UnityEngine;

public class PhysisPlayDiskAction : SSAction
{
    float speed; // ˮƽ�ٶ�
    Vector3 direction; // ���з���

    public static PhysisPlayDiskAction GetSSAction(Vector3 direction, float speed)
    {
        PhysisPlayDiskAction action = ScriptableObject.CreateInstance<PhysisPlayDiskAction>();
        action.speed = speed;
        action.direction = direction;
        return action;
    }

    public override void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        // ˮƽ���ٶ�
        gameObject.GetComponent<Rigidbody>().velocity = speed * direction;
    }

    public override void Update()
    {
        // �ɵ�����ײ������������ص�
        if (this.transform.position.y < -5)
        {
            this.destroy = true;
            this.enable = false;
            this.callback.SSActionEvent(this);
        }
    }
}