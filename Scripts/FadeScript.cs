using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    float _transparentTimer;
   
    void Update()
    {
        _transparentTimer = Mathf.Clamp(_transparentTimer, 0, 1);
        _transparentTimer += .01f;
        GetComponent<Image>().color = new Color(0, 0, 0, _transparentTimer);

        if (_transparentTimer >= 1) SceneManager.LoadScene("GameOver");
    }
}
