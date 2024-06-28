using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeTime;
    public float maxVolume;
    private float targetVolume;

    private void Start()
    {
        targetVolume = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = targetVolume;
        audioSource.Play();
    }

    private void Update()
    {
        if(!Mathf.Approximately(audioSource.volume, targetVolume))
        // Approximately : 근삿값 범위 안에 있다면 같은 값으로 인식할 수 있게 도와줌
        // 부정하였으니 근삿값이 아닐때 라는 조건이 됨
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (maxVolume / fadeTime) * Time.deltaTime);
            // MoveTowards : 점진적으로 볼륨을 올려줌
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = maxVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0f;
        }
    }
}
