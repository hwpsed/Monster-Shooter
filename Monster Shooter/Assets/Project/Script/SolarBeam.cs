using UnityEngine;

public class SolarBeam : MonoBehaviour
{
    public Skill skill;
    private float duration;

    void Start()
    {
        duration = skill.getDuration(); 
    }

    void Update()
    {
        duration -= 1 * Time.deltaTime;
        if (duration <= 0)
        {
            skill.canonScript.turnOnCanon();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Monster"))
        {
            MonsterController monsterController = collision.gameObject.GetComponent<MonsterController>();

            if(monsterController.isAlive)
            {
                float damage = skill.damage;
                monsterController.takeDamage(damage);
            }
        }
    }
}
