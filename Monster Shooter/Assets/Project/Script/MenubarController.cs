using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenubarController : MonoBehaviour
{
    public Button exit;
    public Button WeapBut;
    public Button SkillBut;
    public Button ShopBut;
    public Button MoreBut;


    public Sprite WeaponOn;
    public Sprite WeaponOff;
    public Sprite SkillOn;
    public Sprite SkillOff;
    public Sprite Exit;
    void Start()
    {
        WeapBut = GetComponent<Button>();
        SkillBut = GetComponent<Button>();
    }

    public void ChangeButton()
    {
        if (WeapBut.GetComponent<Button>())
            WeapBut.image.sprite = WeaponOff;
        else
        {
            WeapBut.image.sprite = WeaponOn;
        }

        if (SkillBut.image.sprite == SkillOn)
            SkillBut.image.sprite = SkillOff;
        else
        {
            SkillBut.image.sprite = SkillOn;
        }

    }
}
