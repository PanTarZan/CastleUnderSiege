using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public int upgradeCost = 0;
    public int upgradeCostFixedPrice = 50;

    public float damageMultiplier=1;
    public float radiusMultiplier=1;
    public float spellDamageMultiplier=1;
    public float spellCostMultiplier=1;
    public float shootingCooldownMultiplier=1;
    public int totalSumOfUpgrades = 1;


    public TMPro.TextMeshProUGUI upgradeText;
    private HeadQuaters HQ;

    private void Start()
    {
        HQ = FindObjectOfType<HeadQuaters>();
    }
    private void Update()
    {
        upgradeCost = upgradeCostFixedPrice * totalSumOfUpgrades;
        setNewUpgradePriceUI();
    }

    public void increaseDMG()
    {
        if (HQ.currentMoney >= upgradeCost)
        {
            damageMultiplier = damageMultiplier * 1.1f;
            HQ.currentMoney -= upgradeCost;
            totalSumOfUpgrades++;
        }
    }
    public void increaseRADIUS()
    {
        if (HQ.currentMoney >= upgradeCost)
        {
            radiusMultiplier = radiusMultiplier * 1.1f;
            HQ.currentMoney -= upgradeCost;
            totalSumOfUpgrades++;
        }
    }
    public void increaseSPELLDMG()
    {
        if (HQ.currentMoney >= upgradeCost)
        {
            spellDamageMultiplier = spellDamageMultiplier * 1.1f;
            HQ.currentMoney -= upgradeCost;
            totalSumOfUpgrades++;
        }
    }
    public void increaseSPELLCOST()
    {
        if (HQ.currentMoney >= upgradeCost)
        {
            spellCostMultiplier = spellCostMultiplier * 0.9f;
            HQ.currentMoney -= upgradeCost;
            totalSumOfUpgrades++;
        }
    }
    public void increaseCOOLDOWN()
    {
        if (HQ.currentMoney >= upgradeCost)
        {
            shootingCooldownMultiplier = shootingCooldownMultiplier * 0.9f;
            HQ.currentMoney -= upgradeCost;
            totalSumOfUpgrades++;
        }
    }

    public void setNewUpgradePriceUI()
    {
        if(upgradeText)
        upgradeText.text = upgradeCost.ToString();
    }

}
