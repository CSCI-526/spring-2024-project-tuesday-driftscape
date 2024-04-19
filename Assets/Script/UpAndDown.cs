using UnityEngine;

public class ResponsiveStretch : MonoBehaviour
{
    public float cycleDuration = 2f;  // ���ڳ���ʱ�䣬��0����top���ٷ��ص�0��
    public float top = 3f;            // ����������
    private float timer;              // ��ʱ��
    private Vector3 originalScale;    // ԭʼ�ߴ�
    private Vector3 originalPosition; // ԭʼλ��

    void Start()
    {
        originalScale = transform.localScale;  // ����ԭʼ�ߴ�
        originalPosition = transform.localPosition;  // ����ԭʼλ��
    }

    void Update()
    {
        timer += Time.deltaTime; // ���¼�ʱ��
        float cycleFraction = (timer % cycleDuration) / cycleDuration; // ���㵱ǰ���ڵı���
        float currentScaleFactor = Mathf.Lerp(0, top, cycleFraction * 2); // �����ڵ�ǰ��δ�0��top���Բ�ֵ

        if (cycleFraction >= 0.5f)
        {
            currentScaleFactor = Mathf.Lerp(top, 0, (cycleFraction - 0.5f) * 2); // �����ڵĺ��δ�top��0���Բ�ֵ
        }

        // �ж���ת����������Z�����ת�ǶȾ������췽��
        float rotationZ = transform.eulerAngles.z;
        if (Mathf.Approximately(rotationZ, 90f) || Mathf.Approximately(rotationZ, 270f))
        {
            // 90�Ȼ�270����ת����������
            transform.localScale = new Vector3(originalScale.x, originalScale.y * currentScaleFactor, originalScale.z);
            // ����������Ҳ�̶�������xλ�ã����㷽���Ǳ����Ҳ಻��
            transform.localPosition = new Vector3(originalPosition.x - originalScale.x * (currentScaleFactor - 1) / 2, originalPosition.y, originalPosition.z);
        }
        else
        {
            // ��������
            transform.localScale = new Vector3(originalScale.x, originalScale.y * currentScaleFactor, originalScale.z);
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + originalScale.y * (currentScaleFactor - 1) / 2, originalPosition.z);
        }
    }
}
