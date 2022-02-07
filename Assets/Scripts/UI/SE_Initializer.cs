using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Initializer : MonoBehaviour
{
    [SerializeField] AudioSource seAudioSrc;

    //���ݒ�A�I�[�f�B�I���������烊�X�g�����܂�

    //AudioClips(SE��ǉ�����ꍇ�͂����ɕϐ���ǉ�)
    [Header("Audio_Clips")]
    public AudioClip emoteEffect;
    public AudioClip buttonPushed;

    //SE�̉��ʂ��ʂŕς������ꍇ�ϐ���ǉ�
    [Header("Audio_Volume")]
    public float seVolume1;

    // Start is called before the first frame update
    void Start()
    {
        if(seAudioSrc == null)
        {
            Debug.LogError("Audio��������܂���");
            return;
        }
    }
     

    //SE��炵�����Ƃ���ŌĂяo���֐�
    public void AudioPlay(AudioClip clip, float vol)
    {
        seAudioSrc.volume = vol;
        seAudioSrc.PlayOneShot(clip);
    }
}
