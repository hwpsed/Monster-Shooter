using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public new string name;
    public int level;
    public Sprite artwork;
    public Bullet bullet;

    
    public int baseSpeed;
    public int baseCritical;
    public long baseDamage;

    public int speedRate; // Indicate how many speed will increase per level
    public long damageRate; // Indicate how many damage will increase per level
    public int criticalRate; // Indicate how many critical will increase per level

    public long damage;
    public int critical;
    public int speed;

    public int bulletMultiple;
    public bool isUnlocked;
    public int tier;
    public long price;

    public long getUpgradePrice()
    {
        return tier * level * damage;
    }

    public long getDamage()
    {
        return baseDamage + level * damageRate;
    }

    public int getSpeed()
    {
        return Mathf.Min(baseSpeed + level * speedRate, 40); // Speed is never higher than 40
    }

    public int getCritical()
    {
        return Mathf.Min(baseCritical + level * criticalRate, 100); // Critical is never higher than 100%
    }

    public void setLevel(int level)
    {
        if (level <= 0)
            level = 1;

        this.level = level;

        damage = getDamage();
        speed = getSpeed();
        critical = getCritical();

        bullet.SetBulletAttribute(damage, critical);
    }
}
