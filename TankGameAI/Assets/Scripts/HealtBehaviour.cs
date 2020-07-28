using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealtBehaviour : MonoBehaviour
{
    public Text CanText;
    public Image CanBarıOn;
    float can = 100;
    float mod;
    
    public void HasarYap(float amount)
    {
        amount = (can * 20) / 100;
        can -= amount;
        
        CanText.text = string.Format("%{0}", (int) can);        
        CanBarıOn.fillAmount = can / 100f;
        
        if (can<=0.8)
        {
            Destroy(gameObject);
        }
    }
}




