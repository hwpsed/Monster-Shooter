using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject canon;
    public GameObject BG;
    public List<GameObject> listSpawnMonster;
    public List<Gun> listGun;
    public GameObject spawnCenter;
    public float spawnRadius;
    public TMPro.TextMeshProUGUI goldText;
    public GameObject bagCreator;
    public long gold = 0;
    public Image progressBar;
    public int maxProgress = 50;
    public float timeSpawnMonster = 1.0f;
    public GameObject annoucePrefab;
    public Canvas parentCanvas;

    // Default height and width seen by camera at 1280x720 resolution 
    private float heightFactor = 10f;
    private float witdthFactor = 5.625f;
    private Gun currentCanon;
    private CanonController canonScript;
    private int progress = 0;
    private List<GameObject> listCurrentMonster = new List<GameObject>();
    private Queue<(string, float)> announceQueue = new Queue<(string, float)>();
    private bool isAllowAnnounce = true;
    private int GameLevel = 1;
    private const float announceTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        fixScreenSize();
        currentCanon = listGun[0];
        bagCreator.GetComponent<ListContent>().Populate(listCanon: listGun, currentCanon);
        canonScript = canon.GetComponent<CanonController>();
        canonScript.setCanon(currentCanon);
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentCanon.Equals(canonScript.gun))
        {
            canonScript.setCanon(currentCanon);
        }

        progressBar.fillAmount = (float) progress / maxProgress;

        //Handle Annoucement
        handleAnnoucement();
    }

    private void handleAnnoucement()
    {
        if (announceQueue.Count > 0)
        {
            if (isAllowAnnounce)
            {
                (string, float) result = announceQueue.Peek();
                AnnouncementController.CreateAnnouncement(annoucePrefab, parentCanvas, result.Item1, result.Item2);

                announceQueue.Dequeue();
                isAllowAnnounce = false;
            }
        }
    }

    public void FinishAnnoucement()
    {
        isAllowAnnounce = true;
    }
    public void updateGold(long amount)
    {
        gold += amount;
        goldText.text = Helper.displayNumber(gold);
    }

    public void LoseGame()
    {
        CommitAnnoucement("Level Failed ");
        destroyAllCurrentMonster();
        StopCoroutine("spawnMonster");
        StartLevel();
    }

    public void StartLevel()
    {
        progress = 0;
        CommitAnnoucement("Level " + GameLevel.ToString() + " Started ");
        StartCoroutine("spawnMonster");
    }

    IEnumerator spawnMonster()
    {   
        int spawnedMonster = 0;

        while (spawnedMonster < maxProgress)
        {
            GameObject monster = Instantiate(listSpawnMonster[Random.Range(0, listSpawnMonster.Count)], spawnCenter.transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
            listCurrentMonster.Add(monster);
            ++spawnedMonster;

            yield return new WaitForSeconds(timeSpawnMonster);
        }

        yield return null;
    }

    void fixScreenSize()
    {
        // height and width seen by camera
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        spawnRadius = width / 2 * 0.85f;
        BG.transform.localScale = new Vector2(BG.transform.localScale.x * width / witdthFactor, BG.transform.localScale.y* height / heightFactor);
    }

    public void CommitAnnoucement(string content, float time = announceTime)
    {
        announceQueue.Enqueue((content, time));
    }

    public void changeCanon(Gun gun)
    {
        currentCanon = gun;
    }

    public void countDeadMonster()
    {
        ++progress;
        if (progress >= maxProgress)
            passLevel();
    }

    public void addGold(long goldAmount)
    {
        gold += goldAmount;
    }

    public long getGold()
    {
        return gold;
    }

    private void destroyAllCurrentMonster()
    {
        foreach (GameObject monster in listCurrentMonster)
        {
            monster.GetComponent<MonsterController>().selfDestroy();
        }
    }
    public void passLevel()
    {
        CommitAnnoucement("Level " + GameLevel.ToString() + " Passed ");
        ++GameLevel;
        StartLevel();
    }

    public void RemoveFromCurrentList(GameObject monster)
    {
        listCurrentMonster.Remove(monster);
    }
}

public static class Helper
{
    public static string displayNumber(long number)
    {
        if (number < Mathf.Pow(10, 3))
            return number.ToString();

        string[] names = { "K", "M", "B", "T", "Qua", "Qui", "Se", "Sep", "O"};
        string result;
        int nameIndex = 0;
        int multiplyFactor = 3;
        float temp;

        while (true)
        {
            temp = number;
            temp /= Mathf.Pow(10, multiplyFactor);

            if (temp < Mathf.Pow(10, 3))
                break;

            multiplyFactor += 3;
            ++nameIndex;
        }

        result = temp.ToString();

        if (nameIndex >= 0)
            result += names[nameIndex];

        return result;
    }

    public static bool getChance(int percent, int max=100)
    {
        if (Random.Range(0, max + 1) <= percent)
            return true;
        return false;
    }

}