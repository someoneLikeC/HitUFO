using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab; // �ɵ���Ϸ���󣬴����µķɵ���Ϸ����ĸ��ƶ���
    private List<DiskData> used; // ���ڱ���Ϸʹ�õķɵ�����
    private List<DiskData> free; // û�б�ʹ�õĿ��зɵ�����

    public void Start()
    {
        diskPrefab = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Disk"), Vector3.zero, Quaternion.identity);
        diskPrefab.SetActive(false);
        used = new List<DiskData>();
        free = new List<DiskData>();
    }

    // �ɵ���ȡ����������ruler��ȡ��Ӧ�ɵ�
    public GameObject GetDisk(Ruler ruler)
    {
        GameObject disk;

        // �ӻ����л�ȡ�ɵ���û�����ȴ���
        int diskNum = free.Count;
        if (diskNum == 0)
        {
            disk = GameObject.Instantiate(diskPrefab, Vector3.zero, Quaternion.identity);
            disk.AddComponent(typeof(DiskData));
        }
        else
        {
            disk = free[diskNum - 1].gameObject;
            free.Remove(free[diskNum - 1]);
        }

        // ����ruler����disk���ٶȡ���ɫ����С�����뷽��
        disk.GetComponent<DiskData>().speed = ruler.speed;
        disk.GetComponent<DiskData>().color = ruler.color;
        disk.GetComponent<DiskData>().size = ruler.size;

        // ���ɵ�����ɫ
        if (ruler.color == "red")
        {
            foreach (var child in disk.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if (ruler.color == "green")
        {
            foreach (var child in disk.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            foreach (var child in disk.GetComponentsInChildren<Transform>())
            {
                child.GetComponent<Renderer>().material.color = Color.blue;
            }
        }

        // ���Ʒɵ���С
        disk.transform.localScale = new Vector3((float)ruler.size,(float)ruler.size, (float)ruler.size);

        // ѡ��ɵ�������Ļ����ʼλ��
        disk.transform.position = ruler.beginPos;

        // ���÷ɵ���ʾ
        disk.SetActive(true);

        // ���ɵ�����ʹ�ö���
        used.Add(disk.GetComponent<DiskData>());

        return disk;
    }

    // �ɵ����շ���������ʹ�õķɵ���ʹ�ö��зŵ����ж�����
    public void FreeDisk(GameObject disk)
    {
        foreach (DiskData d in used)
        {
            if (d.gameObject.GetInstanceID() == disk.GetInstanceID())
            {
                disk.SetActive(false);
                used.Remove(d);
                free.Add(d);
                break;
            }

        }
    }
}
