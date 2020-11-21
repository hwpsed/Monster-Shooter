using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPrefabController : MonoBehaviour
{
    public Gun canon;

    public TMPro.TextMeshProUGUI gunName;
    public TMPro.TextMeshProUGUI level;
    public TMPro.TextMeshProUGUI unlockPriceText;
    public TMPro.TextMeshProUGUI unlockPriceTextDisable;
    public TMPro.TextMeshProUGUI upgradePriceText;
    public TMPro.TextMeshProUGUI upgradePriceTextDisable;

    public Image artwork;
    public Text damage;
    public Text speed;
    public Text multiply;
    public Text critical;
    public GameObject EquipButton;
    public GameObject UnlockButton;
    public GameObject UpgradeButton;
    public GameObject unlockMask;

    public ListContent controller;

    private bool isAbleToEquip;

    // Start is called before the first frame update
    public void Initialize(Gun gun, ListContent controller)
    {
        this.controller = controller;
        canon = gun;
        gunName.text = canon.name;
        level.text = "Level " + canon.level.ToString();
        artwork.sprite = canon.artwork;
        damage.text = canon.damage.ToString();
        speed.text = canon.speed.ToString();
        multiply.text = canon.bulletMultiple.ToString();
        critical.text = canon.critical.ToString() + "%";
        
        isAbleToEquip = true;
        if (canon.isUnlocked)
            unlockMask.SetActive(false);

        // display cost
        unlockPriceText.text = Helper.displayNumber(canon.price);
        unlockPriceTextDisable.text = Helper.displayNumber(canon.price);
    }

    void Update()
    {
        level.text = "Level " + canon.level.ToString();
        damage.text = canon.damage.ToString();
        speed.text = canon.speed.ToString();
        multiply.text = canon.bulletMultiple.ToString();
        critical.text = canon.critical.ToString() + "%";

        // display the upgrade cost of the canon
        long upgradePrice = canon.getUpgradePrice();
        upgradePriceText.text = Helper.displayNumber(upgradePrice);
        upgradePriceTextDisable.text = Helper.displayNumber(upgradePrice);

        if (controller.gameController.getGold() >= upgradePrice)
            UpgradeButton.SetActive(true);
        else
            UpgradeButton.SetActive(false);

        // display the cost of canon
        if (controller.gameController.getGold() >= canon.price)
            UnlockButton.SetActive(true);
        else
            UnlockButton.SetActive(false);

        // check if the canon is unlocked
        if (canon.isUnlocked)
            unlockMask.SetActive(false);
        else
            unlockMask.SetActive(true);
    }

    public void setEquipAvailable(bool value)
    {
        isAbleToEquip = value;
        if (value)
            EquipButton.SetActive(true);
        else
            EquipButton.SetActive(false);
    }
    public void Equip()
    {
        controller.changeCanon(this);
    }

    public void Unlock()
    {
        canon.isUnlocked = true;
        controller.gameController.addGold(-canon.price);
    }

    public void Upgrade()
    {
        canon.setLevel(canon.level + 1);
        controller.gameController.addGold(-canon.getUpgradePrice());
    }
}
