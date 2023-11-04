using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true; // �����ɽ���
    public bool destroy = false; // ��������ɿɱ�����

    public GameObject gameObject { get; set; } // ������Ϸ����
    public Transform transform { get; set; } // ��Ϸ����ĵ��˶�
    public ISSActionCallback callback { get; set; } // �ص�����

    public virtual void Start() { } // Start()��д����

    public virtual void Update() { } // Update()��д����
}