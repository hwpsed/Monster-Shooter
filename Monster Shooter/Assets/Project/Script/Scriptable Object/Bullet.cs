
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public new string name;
    public Sprite artwork;
    public float damage;
    public float speed;
    public float critical;

    public void SetBulletAttribute(float damage, float critical)
    {
        this.damage = damage;
        this.critical = critical;
    }
}
