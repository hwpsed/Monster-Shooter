using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonsterController
{
    public Image healthBar;

    protected override void Update()
    {
        if (!isAlive)
            return;

        rigid.velocity = -transform.up * 0.1f;

        healthBar.fillAmount = (float) currentHP / hitPoint;
        healthText.text = currentHP.ToString() + "/" + hitPoint.ToString();
        if (currentHP <= 0)
            currentHP = 0;

        if (currentHP <= 0)
            selfDestroy();

        
    }

    protected override IEnumerator takeDamageAnim()
    {
        bodyRenderer.color = new Color(0.75f, 0.75f, 0.75f);
        yield return new WaitForSeconds(0.2f);
        bodyRenderer.color = Color.white;
        yield return null;
    }
}
