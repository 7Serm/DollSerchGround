using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    public Material material;  // シェーダーが適用されたマテリアル
    [Range(0, 1), SerializeField]
    private float fadespeed = 0.1f;


    private float fadeAmount = 1;

    void Update()
    {
        fadeAmount-=fadespeed;
        // C#スクリプトからフェード値をシェーダーに設定
        material.SetFloat("_FadeAmount", fadeAmount);
    }
}
