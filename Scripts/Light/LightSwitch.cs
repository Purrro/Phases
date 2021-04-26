using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameObject bulb, switchLever;
    [SerializeField] GameObject player;
    AudioSource audioSource;
    private bool isOn;

    void Start()
    {
        isOn = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        float _dist = Vector3.Distance(player.transform.position, transform.position);

        if (_dist <= 1.5f)
        {
            SetLight();
        }
    }

    public void SetLight()
    {
        isOn = !isOn;
        bulb.SetActive(isOn);
        audioSource.Play();
        switchLever.transform.localEulerAngles = new Vector3(-switchLever.transform.localEulerAngles.x, 0, 0);
    }
}



