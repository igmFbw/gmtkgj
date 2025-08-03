using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class globalManager : MonoBehaviour
{
    public int coin;
    public bool isBuilding;
    [SerializeField] private List<int> enemyList;
    [SerializeField] private List<Transform> enemyBornList;
    [SerializeField] private List<enemy> enemyPrefabList;
    [SerializeField] private tipText tipTextPrefab;
    [SerializeField] private Transform tipTextParent;
    [SerializeField] private int levelIndex;
    [SerializeField] private Text coinText;
    [SerializeField] private AudioSource coinAudio;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private List<float> enemyBornTime;
    [SerializeField] private Text enemyWaveText;
    [SerializeField] private Text enemyNumText;
    public GameObject coinPrefab;
    private int enemyCount;
    private int enemyWave;
    private static globalManager _instance;
    public static globalManager instance
    {
        get
        {
            if (_instance == null || !_instance)
            {
                _instance = FindObjectOfType<globalManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null || !_instance)
        {
            _instance = this as globalManager;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        enemyWaveText.text = "current wave:1";
        enemyNumText.text = "remaining enemies:" + enemyList[0];
        StartCoroutine(bornEnemy(enemyBornTime[0]));
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    public void checkEnemy()
    {
        enemyCount++;
        enemyNumText.text = "remaining enemies:" + (enemyList[enemyWave] - enemyCount);
        if (enemyCount == enemyList[enemyWave])
        {
            enemyWave++;
            enemyCount = 0;
            
            if (enemyWave >= enemyList.Count)
            {
                win();
                enemyWaveText.text = "current wave:" + enemyWave;
                enemyNumText.text = "remaining enemies:" + 0;
            }
            else
            {
                StartCoroutine(bornEnemy(enemyBornTime[enemyWave]));
                enemyWaveText.text = "current wave:" + (enemyWave + 1);
                enemyNumText.text = "remaining enemies:" + (enemyList[enemyWave] - enemyCount);
            }
        }
    }
    private void win()
    {
        Time.timeScale = 0;
        winUI.gameObject.SetActive(true);
    }
    public void lose()
    {
        Time.timeScale = 0;
        loseUI.gameObject.SetActive(true);
    }
    private IEnumerator bornEnemy(float time)
    {
        yield return new WaitForSeconds(5);
        for (int i = 1; i <= enemyList[enemyWave]; i++)
        {
            int n = Random.Range(0,enemyBornList.Count);
            int j = Random.Range(0,enemyPrefabList.Count);
            enemy newEnemy = Instantiate(enemyPrefabList[j], enemyBornList[n].position, Quaternion.identity);
            newEnemy.setLayer(n);
            yield return new WaitForSeconds(time);
        }
    }
    public void acquireCoin(int num)
    {
        coin += num;
        updateCoinText();
        coinAudio.Play();
    }
    public void costCoin(int num)
    {
        coin -= num;
        updateCoinText();
    }
    public void updateCoinText()
    {
        coinText.text = coin.ToString();
    }
    public void setTip(string str)
    {
        tipText newTip = Instantiate(tipTextPrefab, tipTextParent);
        newTip.setString(str);
    }
    public void nextLevel()
    {
        if (levelIndex < 3)
        {
            SceneManager.LoadScene(levelIndex + 1);
            Time.timeScale = 1;
        }
        else
            setTip("It's already the last level");
    }
    public void rePlay()
    {
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }
    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
