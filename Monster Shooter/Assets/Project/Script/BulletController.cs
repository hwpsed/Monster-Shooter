using UnityEngine;


public class BulletController : MonoBehaviour
{
    public Bullet bullet;

    private int critFactor = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bullet.artwork;
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bullet.speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Monster"))
        {
            MonsterController monsterController = collision.gameObject.GetComponent<MonsterController>();

            if (monsterController.isAlive)
            {
                float damage = bullet.damage;
                if (Helper.getChance((int)bullet.critical))
                    damage *= critFactor;

                monsterController.takeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (collision.tag.Equals("DeadZone"))
            Destroy(gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Background"))
            Destroy(gameObject);
    }
}
