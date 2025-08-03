using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class levelChooseButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private int levelIndex;
    [SerializeField] private CanvasGroup whiteImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        whiteImage.gameObject.SetActive(true);
        whiteImage.DOFade(1, 1).OnComplete(() =>
        {
            SceneManager.LoadScene(levelIndex);
        });
    }
}
