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
        DamagePopup dp = dp_trans.GetComponent<DamagePopup>();
        dp.Setup(damageAmount);


        return dp;
    }

    public void Awake()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        float moveYSpeed= 10f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        dissappearTimer -= Time.deltaTime;
        if (dissappearTimer <= 0)
        {
            float dissappearSpeed = 3;
            textColor.a -= dissappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
