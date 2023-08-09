using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName = "StreetScene"; // Имя сцены, которую нужно загрузить
    
    public void LoadScene()
    {
        // Загружаем сцену по её имени
        SceneManager.LoadScene(sceneName);
    }
}
