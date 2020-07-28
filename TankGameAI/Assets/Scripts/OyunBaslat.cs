using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunBaslat : MonoBehaviour
{
    public int sahneId;
    public void sahneDegis(int sahneId)
    {
        SceneManager.LoadScene(sahneId);
    }
}
