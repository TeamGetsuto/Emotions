using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Initializer : MonoBehaviour
{
    [SerializeField] AudioSource seAudioSrc;

    //仮設定、オーディオが増えたらリスト化します

    //AudioClips(SEを追加する場合はここに変数を追加)
    [Header("Audio_Clips")]
    public AudioClip emoteEffect;
    public AudioClip buttonPushed;

    //SEの音量を個別で変えたい場合変数を追加
    [Header("Audio_Volume")]
    public float seVolume1;

    // Start is called before the first frame update
    void Start()
    {
        if(seAudioSrc == null)
        {
            Debug.LogError("Audioが見つかりません");
            return;
        }
    }
     

    //SEを鳴らしたいところで呼び出す関数
    public void AudioPlay(AudioClip clip, float vol)
    {
        seAudioSrc.volume = vol;
        seAudioSrc.PlayOneShot(clip);
    }
}
