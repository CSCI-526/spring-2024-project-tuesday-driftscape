using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material propertyMaterial = spriteRenderer.material;

        // ��ȡ����ĳߴ�
        float width = transform.localScale.x;
        float height = transform.localScale.y;

        // ����Ӧ���ظ�����Ĵ���
        Vector2 tiling = new Vector2(width, height);

        // ���ò��ʵ�Tiling
        propertyMaterial.mainTextureScale = tiling;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
