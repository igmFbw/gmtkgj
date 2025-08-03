using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class endUI : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    private void OnEnable()
    {
        audioPlayer.Play();
    }
}
