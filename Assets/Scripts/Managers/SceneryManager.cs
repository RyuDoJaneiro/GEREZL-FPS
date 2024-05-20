using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private GameObject _mainMenu; 

    private void Awake()
    {
        _loadingScreen = gameObject.transform.Find("Loading Canvas/Loading Screen View").gameObject;
        _victoryScreen = gameObject.transform.Find("Main Canvas/Victory View").gameObject;
        _loseScreen = gameObject.transform.Find("Main Canvas/Lose View").gameObject;
        _loadingBar = gameObject.transform.Find("Loading Canvas/Loading Screen View/Loading Bar").gameObject.GetComponent<Slider>();
        _mainMenu = gameObject.transform.Find("Main Canvas/Main Menu View").gameObject;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public void ChangeScene(int sceneIndex)
    {
        _loadingScreen.SetActive(true);
        Debug.Log($"{name}: Changing to scene with build index {sceneIndex}");
        StartCoroutine(LoadAsync(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone) 
        {
            _loadingBar.value = asyncOperation.progress;
            yield return null;
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _loadingScreen.SetActive(false);
    }

    public void Victory()
    {
        _victoryScreen.SetActive(true);
        _mainMenu.SetActive(true);

        SceneManager.LoadScene("MainMenu");
    }

    public void Lose()
    {
        _loseScreen.SetActive(true);
        _mainMenu.SetActive(true);

        SceneManager.LoadScene("MainMenu");
    }
}
