using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public new string name;
    public int level;
    public GameObject Canon;
    public GameObject skillPrefab;

    public long priceMul = 50000;
    public int damageMul = 50;
    public float damage;
    public float duration;
    public float cooldown;


    public long price;
    public bool isUnlocked;
    public CanonController canonScript;

    public void Awake()
    {
        Canon = GameObject.FindGameObjectWithTag("Canon");
        canonScript = Canon.GetComponent<CanonController>();
    }

    public long getUpdatePrice()
    {
        return price * level * priceMul;
    }

    public float getDamage()
    {
        float gunDamage = canonScript.gun.getDamage();
        return gunDamage * damageMul * level;
    }

    public float getDuration()
    {
        return duration ;
    }

    public float getCooldown()
    {
        return cooldown;
    }

    public void setLevel(int level)
    {
        if(level <= 0)
            level = 1;
        this.level = level;

        price = getUpdatePrice();
        damage = getDamage();
        duration = getDuration();
        cooldown = getCooldown();
    }

    public void CastSkill()
    {
        //Canon = GameObject.FindGameObjectWithTag("Canon");
        //canonScript = Canon.GetComponent<CanonController>();
        Instantiate(skillPrefab, canonScript.head.transform);
        canonScript.turnOffCanon();
    }
}
