using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{
    [Header("Audio Property")]
    AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float waitTime;
    bool isAudioPlay;

    Player_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.direction != Vector3.zero)
        {
           if(isAudioPlay)
            {
                return;
            }

            StartCoroutine("AudioPlay");
        }
    }
    IEnumerator AudioPlay()
    {
        isAudioPlay = true;

        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(waitTime);

        audioSource.Stop();

        isAudioPlay = false;
    }
}
