using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class CanonController : MonoBehaviour
{
    public Gun gun;
    public GameObject head;

    //Bullet
    public GameObject bulletPrefab;
    private Bullet bullet;

    //Skill
    public List<Skill> skillList;
    public Skill selectedSkill;
    public GameObject skillPrefab;
    private Skill skill;
    //============
    private float shootDelay;
    private float skewAngle = 10f;
    private bool isCasting = false;

    void Start()
    {
    }

    public void setCanon(Gun gun)
    {
        StopAllCoroutines();
        this.gun = gun;
        GetComponent<SpriteRenderer>().sprite = gun.artwork;
        bullet = gun.bullet;
        gun.setLevel(gun.level);
        StartCoroutine("Shoot");
        
    }

    void Update()
    {
        shootDelay = 0.5f - gun.speed / 100f;
        bullet.SetBulletAttribute(gun.damage, gun.critical);

        faceToPoint(Input.mousePosition);
        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        faceToPoint(touch.position);
        //    }
        //}
    }

    void faceToPoint(Vector2 point)
    {
        point = Camera.main.ScreenToWorldPoint(point);
        Vector2 direction = new Vector2(point.x - transform.position.x, point.y - transform.position.y);

        transform.up = direction;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            for (int i = -gun.bulletMultiple / 2; i <= gun.bulletMultiple / 2; i++)
            {
                GameObject tempBullet = Instantiate(bulletPrefab, head.transform.position, transform.rotation * Quaternion.Euler(0, 0, skewAngle * i));

                BulletController bulletScript = tempBullet.GetComponent<BulletController>();
                bulletScript.bullet = bullet;
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }

    public void CastSkill()
    {
        if (!isCasting)
        {
            isCasting = true;
            selectedSkill.Awake();
            selectedSkill.CastSkill();
        }
            
    }

    public void turnOffCanon()
    {
        StopCoroutine("Shoot");
    }

    public void turnOnCanon()
    {
        turnOffCanon();
        isCasting = false;
        StartCoroutine("Shoot");
    }
}
