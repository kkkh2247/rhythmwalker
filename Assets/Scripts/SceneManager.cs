using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    public enum SceneState
    {
        TITLE = 0,
        MAIN = 1
    };

    public SceneState scene;

    public static SceneManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        scene = SceneState.TITLE;
    }

    public void LoadScene(SceneState s)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(s.ToString());
    }

    public void MoveGameObjectToScene(GameObject obj, SceneState s)
    {
        UnityEngine.SceneManagement.Scene ss = UnityEngine.SceneManagement.SceneManager.GetSceneAt(0);
        UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(obj, ss);
        DontDestroyOnLoad(obj);
        UnityEngine.SceneManagement.SceneManager.LoadScene(s.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
