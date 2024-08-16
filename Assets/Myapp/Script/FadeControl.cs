using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    public Material material;  // �V�F�[�_�[���K�p���ꂽ�}�e���A��
    [Range(0, 1), SerializeField]
    private float fadespeed = 0.1f;


    private float fadeAmount = 1;

    void Update()
    {
        fadeAmount-=fadespeed;
        // C#�X�N���v�g����t�F�[�h�l���V�F�[�_�[�ɐݒ�
        material.SetFloat("_FadeAmount", fadeAmount);
    }
}
