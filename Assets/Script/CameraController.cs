using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.0f;
    public float pauseDuration = 2.0f; // 暂停在终点的时间（秒）
    public CameraFollow cameraFollow; // 引用 CameraFollow 脚本

    private void Start()
    {
        if (cameraFollow != null)
            cameraFollow.enabled = false; // 开始时禁用 CameraFollow

        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        // 从当前位置移动到终点
        yield return StartCoroutine(MoveToPosition(transform.position, endPoint.position));
        
        // 在终点暂停一段时间
        yield return new WaitForSeconds(pauseDuration);
        
        // 从终点移动回起点
        yield return StartCoroutine(MoveToPosition(endPoint.position, startPoint.position));

        if (cameraFollow != null)
            cameraFollow.enabled = true; // 重新启用 CameraFollow
    }

    private IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        float journey = 0.0f;
        while (journey <= 1.0f)
        {
            journey += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(fromPosition, toPosition, journey);
            yield return null;
        }
    }
}
