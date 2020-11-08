using UnityEngine;
using UnityEngine.UI;

public class CastingSpells : MonoBehaviour
{

    [SerializeField] float maxMana = 0;
    public float currentMana;
    public bool generateMana = false;
    public float manaRate = 0f;
    public float manaSpeed = 0f;
    float nextMana = 0.1f;
    public UpgradeSystem uSystem;

    Pointer pointer;
    public Vector3 SpellSpawnOffset;

    public float currentSpellCost = 40;
    [SerializeField] Image mana_display_bar = null;
    [SerializeField] GameObject CurrentMagicSpellPrefab;
     public Text mana_display_value;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = 0f;
        pointer = FindObjectOfType<Pointer>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMana();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentSpellCost*uSystem.spellCostMultiplier <= currentMana)
            {
                var spell = Instantiate(CurrentMagicSpellPrefab, pointer.transform.position+SpellSpawnOffset, Quaternion.Euler(90,0,0));
                ApplyUpgrades(spell);
                currentMana -= currentSpellCost*uSystem.spellCostMultiplier;
            }
            else
            {
               // Debug.Log("Not Enough Mana");
            }
        }
        
    }

    private void ApplyUpgrades(GameObject spell)
    {
        var s_projectile = spell.GetComponent<Projectile>();
        s_projectile.damageMIN = s_projectile.damageMIN * uSystem.spellDamageMultiplier;
        s_projectile.damageMAX = s_projectile.damageMAX * uSystem.spellDamageMultiplier;
        s_projectile.explosionRadius = s_projectile.explosionRadius * uSystem.radiusMultiplier;
    }

    private void GenerateMana()
    {
        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
            return;
        }

        if (generateMana)
        {
            if (Time.time >= nextMana)
            {
                nextMana = Time.time + manaSpeed;
                currentMana += manaRate;
            }
        }
        mana_display_bar.fillAmount = currentMana / maxMana;
        mana_display_value.text = currentMana.ToString(); ;
    }

    public void StartGeneratingMana()
    {
        generateMana = true;
    }
}
