using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class blackImage : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    private void Start()
    {
        cg.DOFade(0, 1).OnComplete(()=>
        {
            Destroy(gameObject);
        });
    }
}
