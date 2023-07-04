using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityGamesToolkit.Runtime;

public class MenuButton : MonoBehaviour
{

// Defines variables and properties
#region Variables & Properties

[SerializeField] private string sceneToOpen;

[SerializeField] private List<GameObject> gameObjectsToActivate;

[SerializeField] private List<GameObject> gameObjectsToDeactivate;

[SerializeField] private List<GameObject> gameObjectsToChangeActivation;

#endregion

// Defines methods for the new script
#region Methods

public void OpenScene()
{
    SceneManager.LoadScene(sceneToOpen);
}

public void ChangeObjectInScene()
{
    foreach (GameObject m_object in gameObjectsToActivate)
    {
        m_object.SetActive(true);
    }

    foreach (GameObject m_object in gameObjectsToDeactivate)
    {
        m_object.SetActive(false);
    }
    
    foreach (GameObject m_object in gameObjectsToChangeActivation)
    {
        m_object.SetActive(!m_object.activeSelf);
    }
}

public void QuitGame()
{
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
}

public void ChangeInteractivable(MenuButton  button)
{
    button.GetComponent<Button>().interactable = !button.GetComponent<Button>().interactable;
}

public void ChangeLanguage(S_Language newLanguage)
{
    
}

#endregion

}
