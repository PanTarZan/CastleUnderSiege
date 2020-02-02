using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    [SerializeField] float dissappearTimer = 2f;
    private Color textColor;

    public  DamagePopup Create(Vector3 poisiton, float damageAmount, GameObject dp_prefab)
    {
        var dp_trans = Instantiate(dp_prefab, poisiton, Quaternion.identity);
        dp_trans.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        DamagePopup dp = dp_trans.GetComponent<DamagePopup>();
        dp.Setup(damageAmount);


        return dp;
    }

    public void Awake()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(((int)damageAmount).ToString());
        
    }

    private void Update()
    {
        float moveYSpeed= 10f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        
        dissappearTimer -= Time.deltaTime;
        if (dissappearTimer < 0)
        {
            float dissappearSpeed = 3f;
            textColor.a -= dissappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
