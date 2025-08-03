using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource mouseAudio;
    public void exitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseAudio.Play();
        }
    }
}
