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

        // 获取对象的尺寸
        float width = transform.localScale.x;
        float height = transform.localScale.y;

        // 计算应该重复纹理的次数
        Vector2 tiling = new Vector2(width, height);

        // 设置材质的Tiling
        propertyMaterial.mainTextureScale = tiling;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
