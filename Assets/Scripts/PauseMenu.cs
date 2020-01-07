using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    public Text TimerText;
    [SerializeField]
    private float mainTimer;

    private float timer;
    private bool Countable = true;
    private bool executeOnce = false;
    private bool Paused = false;
    private int click = 0;

    private static float prevRealTime;
    private float thisRealTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {
        prevRealTime = thisRealTime;
        thisRealTime = Time.realtimeSinceStartup;

        if (Paused == true) {
            if (timer >= 0.0f && Countable)
            {
                timer -= cutsceneDeltaTime;
                TimerText.text = timer.ToString("F");
            }
            else if (timer <= 0.0f && !executeOnce)
            {
                Countable = false;
                executeOnce = true;
                TimerText.text = "0.00";
                timer = 0.0f;
            }

        }
    }
   
    public static float cutsceneDeltaTime
    {

        get
        {

            if (Time.timeScale > 0f)
                return Time.deltaTime / Time.timeScale;

            return Time.realtimeSinceStartup - prevRealTime;
        }
    }

    public void Pause()
    {
        if (click == 0)
        {
            Time.timeScale = 0;
            Paused = true;
            click += 1;
        }
        else if (click == 1)
        {
            Paused = false;
            click -= 1;
            timer = 30;
            Time.timeScale = 1;
        }
        
    }
}
 