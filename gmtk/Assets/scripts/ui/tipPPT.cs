using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tipPPT : MonoBehaviour
{
    [SerializeField] private List<GameObject> imageList;
    private GameObject currentPPT;
    private int n;
    private void Start()
    {
        n = 0;
        currentPPT = imageList[0];
    }
    public void addIndex()
    {
        if (n == 2)
            return;
        n++;
        currentPPT.SetActive(false);
        currentPPT = imageList[n];
        currentPPT.SetActive(true);
    }
    public void removeIndex()
    {
        if (n == 0)
            return;
        n--;
        currentPPT.SetActive(false);
        currentPPT = imageList[n];
        currentPPT.SetActive(true);
    }
}
