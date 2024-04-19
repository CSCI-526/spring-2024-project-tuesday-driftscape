using UnityEngine;

public class ResponsiveStretch : MonoBehaviour
{
    public float cycleDuration = 2f;  // 周期持续时间，从0倍到top倍再返回到0倍
    public float top = 3f;            // 拉伸的最大倍数
    private float timer;              // 计时器
    private Vector3 originalScale;    // 原始尺寸
    private Vector3 originalPosition; // 原始位置

    void Start()
    {
        originalScale = transform.localScale;  // 保存原始尺寸
        originalPosition = transform.localPosition;  // 保存原始位置
    }

    void Update()
    {
        timer += Time.deltaTime; // 更新计时器
        float cycleFraction = (timer % cycleDuration) / cycleDuration; // 计算当前周期的比例
        float currentScaleFactor = Mathf.Lerp(0, top, cycleFraction * 2); // 在周期的前半段从0到top线性插值

        if (cycleFraction >= 0.5f)
        {
            currentScaleFactor = Mathf.Lerp(top, 0, (cycleFraction - 0.5f) * 2); // 在周期的后半段从top到0线性插值
        }

        // 判断旋转度数，根据Z轴的旋转角度决定拉伸方向
        float rotationZ = transform.eulerAngles.z;
        if (Mathf.Approximately(rotationZ, 90f) || Mathf.Approximately(rotationZ, 270f))
        {
            // 90度或270度旋转，横向拉伸
            transform.localScale = new Vector3(originalScale.x, originalScale.y * currentScaleFactor, originalScale.z);
            // 横向拉伸的右侧固定，调整x位置，计算方法是保持右侧不动
            transform.localPosition = new Vector3(originalPosition.x - originalScale.x * (currentScaleFactor - 1) / 2, originalPosition.y, originalPosition.z);
        }
        else
        {
            // 纵向拉伸
            transform.localScale = new Vector3(originalScale.x, originalScale.y * currentScaleFactor, originalScale.z);
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + originalScale.y * (currentScaleFactor - 1) / 2, originalPosition.z);
        }
    }
}
