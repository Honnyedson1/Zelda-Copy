using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Cutscene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(2);
    }
}
