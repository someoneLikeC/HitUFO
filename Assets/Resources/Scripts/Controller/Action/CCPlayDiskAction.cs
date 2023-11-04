using UnityEngine;

public class CCPlayDiskAction : SSAction
{
    float gravity; // ��ֱ�ٶ�
    float speed; // ˮƽ�ٶ�
    Vector3 direction;  // ����
    float time; // ʱ��

    public static CCPlayDiskAction GetSSAction(Vector3 direction, float speed)
    {
        CCPlayDiskAction action = ScriptableObject.CreateInstance<CCPlayDiskAction>();
        action.gravity = 9.8f;
        action.time = 0;
        action.speed = speed;
        action.direction = direction;
        return action;
    }

    public override void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void Update()
    {
        time += Time.deltaTime;
        transform.Translate(Vector3.down * gravity * time * Time.deltaTime);
        transform.Translate(direction * speed * Time.deltaTime);
        // �ɵ�����ײ������������ص�
        if (this.transform.position.y < -5)
        {
            this.destroy = true;
            this.enable = false;
            this.callback.SSActionEvent(this);
        }
    }
}
