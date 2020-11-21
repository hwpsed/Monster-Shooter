using System.Collections;
using UnityEngine;

public class AnnouncementController : MonoBehaviour
{
    float waitingTime;
    public TMPro.TextMeshProUGUI content;
    public static GameController gameController;

    public void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void Initialize(string text, float time)
    {
        content.text = text;
        waitingTime = time;
    }

    public void FinishExpand()
    {
        StartCoroutine("StartShrink");
    }

    IEnumerator StartShrink()
    {
        yield return new WaitForSeconds(waitingTime);

        GetComponent<Animator>().Play("Shrink");
        yield return null;
    }
    public void FinishShrink()
    {
        gameController.FinishAnnoucement();
        Destroy(gameObject);
    }

    public static GameObject CreateAnnouncement(GameObject annoucePrefab, Canvas Parent, string text, float time)
    {
        GameObject annnoucement = Instantiate(annoucePrefab, Parent.transform);
        annnoucement.GetComponentInChildren<AnnouncementController>().Initialize(text, time);

        return annnoucement;
    }
}
