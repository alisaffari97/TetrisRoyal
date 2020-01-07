using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public int[] scene;
    public int ActualScene;

  
    public void SelectScene()
    { 
        
        SceneManager.LoadScene(scene[ActualScene]);
    }
}