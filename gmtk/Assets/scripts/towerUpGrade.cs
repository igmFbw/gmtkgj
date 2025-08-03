using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class towerUpGrade : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Text costText;
    [SerializeField] private List<upGradeSlot> gradeSlotList;
    [SerializeField] private GameObject upGradeChooseUI;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private Animator anim;
    private int blueProbability;
    private int purpleProbability;
    private int goldenProbability;
    private int count;
    private int quality;
    private int type;
    private int cost;
    private int[] gunAttack = { 3, 5, 10 };
    private int[] archerAttack = { 1, 2, 4 };
    private int[] furnaceAttack = { 1, 2, 3 };
    private float[] gunCool = { 0.15f, 0.3f, 0.5f };
    private float[] archerCool = { 0.1f, 0.2f, 0.3f };
    private float[] furnaceCool = { 0.1f, 0.2f, 0.3f };
    private List<int> ordinaryUpGradeList;
    private List<int> specialUpGradeList;
    private void Awake()
    {
        ordinaryUpGradeList = new List<int>();
        specialUpGradeList = new List<int>();
    }
    private void randomEffect()
    {
        calculateProbability();
        int n = Random.Range(0, 2);
        if(n == 0)
        {
            calculateOrdinaryUpGrade();
        }
        else
        {
            calculateSpecialUpGrade();
        }
    }
    private void calculateOrdinaryUpGrade()
    {
        ordinaryUpGradeList.Clear();
        calculateQuality();
        while (ordinaryUpGradeList.Count < 3)
        {
            int j = Random.Range(1, 7);
            if (j == 2 && mapManager.instance.gunTowerAttackCool)
                continue;
            else if (j == 4 && mapManager.instance.archerTowerAttackCool)
                continue;
            else if (j == 6 && mapManager.instance.furnaceTowerAttackCool)
                continue;
            if (!ordinaryUpGradeList.Contains(j))
                ordinaryUpGradeList.Add(j);
        }
        type = 1;
    }
    private void calculateSpecialUpGrade()
    {
        specialUpGradeList.Clear();
        calculateQuality();
        if(quality == 0)
        {
            specialUpGradeList.Add(7);
            specialUpGradeList.Add(8);
            specialUpGradeList.Add(9);
        }
        else if(quality == 1)
        {
            specialUpGradeList.Add(10);
            specialUpGradeList.Add(11);
            specialUpGradeList.Add(12);
        }
        else
        {
            specialUpGradeList.Add(13);
            specialUpGradeList.Add(14);
            specialUpGradeList.Add(15);
        }
        type = 2;
    }
    private void calculateQuality()
    {
        int n = Random.Range(1, 101);
        if (blueProbability >= n)
            quality = 0;
        else if (purpleProbability >= n)
            quality = 1;
        else
            quality = 2;
    }
    private void calculateProbability()
    {
        blueProbability = 100 - count * 10;
        if (count <= 10)
            purpleProbability = count * 8;
        else
            purpleProbability = 80 - (count - 10) * 8;
        goldenProbability = count * 2;
    }
    private void openChooseUI()
    {
        upGradeChooseUI.SetActive(true);
        anim.SetBool("isPlay",true);
        Time.timeScale = 0;
        if (type == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                gradeSlotList[i].setColor(quality, ordinaryUpGradeList[i]);
                if(ordinaryUpGradeList[i] == 1)
                {
                    gradeSlotList[i].setText("Enhance the" + gunAttack[quality] 
                        + "point attack power of all turrets");
                }
                else if (ordinaryUpGradeList[i] == 2)
                {
                    gradeSlotList[i].setText("Improve the " + gunCool[quality] 
                        + "point attack speed of all turrets");
                }
                else if (ordinaryUpGradeList[i] == 3)
                {
                    gradeSlotList[i].setText("Enhance the" + archerAttack[quality]
                        + "point attack power of all archer towers");
                }
                else if (ordinaryUpGradeList[i] == 4)
                {
                    gradeSlotList[i].setText("Improve the " + archerCool[quality]
                        + "point attack speed of all archer towers");
                }
                else if (ordinaryUpGradeList[i] == 5)
                {
                    gradeSlotList[i].setText("Enhance the" + furnaceAttack[quality]
                        + "point attack power of all furnace towers");
                }
                else if (ordinaryUpGradeList[i] == 6)
                {
                    gradeSlotList[i].setText("Improve the " + furnaceCool[quality]
                        + "point attack speed of all furnace towers");
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                gradeSlotList[i].setColor(quality, specialUpGradeList[i]);
                if (specialUpGradeList[i] == 7)
                {
                    gradeSlotList[i].setText("After each attack, the turret's attack power " +
                        "increases by one percent");
                }
                else if (specialUpGradeList[i] == 8)
                {
                    gradeSlotList[i].setText("The shell hitting the enemy will slow them down by 30% for three seconds");
                }
                else if (specialUpGradeList[i] == 9)
                {
                    gradeSlotList[i].setText("If there is only one enemy within the attack range of the furnace, deal an additional 50% damage to that enemy");
                }
                else if (specialUpGradeList[i] == 10)
                {
                    gradeSlotList[i].setText("The shells from the turret hitting the enemy will cause an explosion and damage, splashing around the enemies");
                }
                else if (specialUpGradeList[i] == 11)
                {
                    gradeSlotList[i].setText("The arrows of the bow tower have a penetrating effect, but only deal 20% damage to subsequent targets");
                }
                else if (specialUpGradeList[i] == 12)
                {
                    gradeSlotList[i].setText("The arrows from the bow tower hitting the enemy will slow them down by 30%");
                }
                else if (specialUpGradeList[i] == 13)
                {
                    gradeSlotList[i].setText("The shell damage of the turret has been increased by 20%");
                }
                else if (specialUpGradeList[i] == 14)
                {
                    gradeSlotList[i].setText("When the archer tower attacks, fire an additional arrow 30 degrees on each side of the attack direction and deal 25% damage to the original arrow");
                }
                else
                {
                    gradeSlotList[i].setText("Furnace damage increased by 30%");
                }
            }
        }
    }
    public void upGrade(int num)
    {
        if(num == 1)
        {
            foreach(var item in mapManager.instance.gunTowerList)
                item.addAttackPower(gunAttack[quality]);
        }
        else if(num == 2)
        {
            foreach (var item in mapManager.instance.gunTowerList)
                item.addVelocity(gunCool[quality]);
        }
        else if(num == 3)
        {
            foreach(var item in mapManager.instance.archerTowerList)
                item.addAttackPower(archerAttack[quality]);
        }
        else if(num == 4)
        {
            foreach (var item in mapManager.instance.archerTowerList)
                item.addAttackPower(archerAttack[quality]);
        }
        else if(num == 5)
        {
            foreach (var item in mapManager.instance.furnaceTowerList)
                item.addAttackPower(furnaceAttack[quality]);
        }
        else if(num == 6)
        {
            foreach(var item in mapManager.instance.furnaceTowerList)
                item.addVelocity(furnaceCool[quality]);
        }
        else if(num == 7)
        {
            foreach (var item in mapManager.instance.gunTowerList)
                item.isOverLoad = true;
        }
        else if(num == 8)
        {
            foreach (var item in mapManager.instance.gunTowerList)
                item.isCold = true;
        }
        else if(num == 9)
        {
            foreach(var item in mapManager.instance.furnaceTowerList)
                item.isSingle = true;
        }
        else if(num == 10)
        {
            foreach (var item in mapManager.instance.gunTowerList)
                item.isBlast = true;
        }
        else if(num == 11)
        {
            foreach(var item in mapManager.instance.archerTowerList)
                item.isPenetrate = true;
        }
        else if(num == 12)
        {
            foreach (var item in mapManager.instance.archerTowerList)
                item.isCold = true;
        }
        else if(num == 13)
        {
            foreach (var item in mapManager.instance.gunTowerList)
                item.isMortar = true;
        }
        else if(num == 14)
        {
            foreach(var item in mapManager.instance.archerTowerList)
                item.isDivision = true;
        }
        else
        {
            foreach(var item in mapManager.instance.furnaceTowerList)
                item.hance = true;
        }
        audioPlayer.Play();
        count++;
        globalManager.instance.costCoin(cost);
        Time.timeScale = 1;
        cost = 100 + count * 100;
        costText.text = cost.ToString() + "G";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (globalManager.instance.coin < cost)
        {
            globalManager.instance.setTip("You don't have enough coins");
            return;
        }
        randomEffect();
        openChooseUI();
    }
    public void playEnd()
    {
        anim.SetBool("isPlay",false);
    }
}
