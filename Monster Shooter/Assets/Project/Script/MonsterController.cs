using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float hitPoint;

    [Header("Stuff")]
    public Image bar;
    public Text healthText;
    public bool isAlive;
    public GameObject body;
    public GameObject canvas;
    public GameController gameController;

    // -------------Private----------
    protected float currentHP;
    protected Rigidbody2D rigid;
    protected SpriteRenderer bodyRenderer;

    private void Start()
    {
        currentHP = hitPoint;
        isAlive = true;
        rigid = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isAlive)
            return;

        rigid.velocity = -transform.up;

        bar.transform.localScale = new Vector3((float)currentHP / hitPoint, bar.transform.localScale.y, bar.transform.localScale.z);
        healthText.text = currentHP.ToString();

        if (currentHP <= 0)
            selfDestroy();
    }

    public void takeDamage(float damage)
    {
        StopAllCoroutines();
        StartCoroutine("takeDamageAnim");
        currentHP -= damage;
        gameController.updateGold((long)damage);
    }

    protected virtual IEnumerator takeDamageAnim()
    {
        bodyRenderer.color = new Color(0.75f, 0.75f, 0.75f);
        if(canvas != null)
            canvas.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        bodyRenderer.color = Color.white;
        if (canvas != null)
            canvas.SetActive(false);
        yield return null;
    }

    public void selfDestroy()
    {
        isAlive = false;
        Destroy(canvas);
        GetComponent<Animator>().Play("Dead");
        gameController.countDeadMonster();
    }

    public void Remove()
    {
        gameController.RemoveFromCurrentList(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive)
            return;
        if (collision.tag.Equals("DeadZone"))
            gameController.LoseGame();
    }
}
