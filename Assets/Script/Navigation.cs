using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public bool getconfused = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        // ���ʹagent���
        agent.enabled = false;
    }

    void Update()
    {
        // ��Ҵ�����agentδ�����һ�ֹͣ
        if (player != null && !getconfused)
        {
            agent.speed = 3.0f;
            // ���agent�Ƿ����������Ұ��
            if (IsAgentVisibleToCamera() && !agent.enabled)
            {
                // ����agent������Ŀ��Ϊ���λ��
                agent.enabled = true;
                agent.SetDestination(player.position);
            }
            else if (agent.enabled)
            {
                Debug.Log(player.position);
                // ���agent�Ѽ�������׷�����
                agent.SetDestination(player.position);
            }
        }
    }

    bool IsAgentVisibleToCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) // ȷ��Renderer�������
        {
            Bounds bounds = renderer.bounds;
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
        else
        {
            Debug.LogWarning("Renderer not found on " + gameObject.name);
            return false;
        }
    }
}
