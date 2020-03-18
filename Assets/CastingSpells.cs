using System.Collections;
using System.Collections.Generic;
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

    public float currentSpellCost = 40;
    [SerializeField] Image mana_display = null;

    // Start is called before the first frame update
    void Start()
    {

        currentMana = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMana();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentSpellCost <= currentMana)
            {
                Debug.Log("Bum");
                currentMana -= currentSpellCost;
            }
            else
            {
                Debug.Log("Not Enough Mana");
            }
        }
        
    }

    private void GenerateMana()
    {
        if (generateMana)
        {
            if (Time.time >= nextMana)
            {
                nextMana = Time.time + manaSpeed;
                currentMana += manaRate;
            }
        }
        mana_display.fillAmount = currentMana / maxMana;
    }

    public void StartGeneratingMana()
    {
        generateMana = true;
    }
}
