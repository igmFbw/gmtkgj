using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class tipText : MonoBehaviour
{
    [SerializeField] private Text textString;
    public void setString(string text)
    {
        textString.text = text;
    }
    private void Start()
    {
        Color newColor = Color.white;
        newColor.a = 0;
        textString.DOColor(Color.white, 1).From().OnComplete(()=>
        {
            Destroy(gameObject);
        });
    }
}
