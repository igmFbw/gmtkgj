using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class upGradeSlot : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private towerUpGrade gradeUI;
    [SerializeField] private Text stringText;
    [SerializeField] private Image image;
    [SerializeField] private GameObject upGradeBack;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Text qualityText;
    private string[] qualityString = { "ordinary", "epic", "legend" };
    private int index;

    public void OnPointerClick(PointerEventData eventData)
    {
        gradeUI.upGrade(index);
        upGradeBack.SetActive(false);
    }
    public void setColor(int i,int index)
    {
        image.sprite = images[i];
        this.index = index;
        qualityText.text = qualityString[i];
    }
    public void setText(string text)
    {
        stringText.text = "\u3000" + text;
    }
}
