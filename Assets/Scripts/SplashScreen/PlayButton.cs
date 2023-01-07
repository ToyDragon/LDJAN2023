using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public void PointerEnter()
    {
        text.color = Color.red;
    }
    public void PointerExit()
    {
        text.color = Color.white;
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
