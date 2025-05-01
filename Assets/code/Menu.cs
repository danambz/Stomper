using UnityEngine; // this is Unity Engine library so I can use functions and classes that are built for unity such as MonoBehaviour and Start() function and 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // I used public variable so I can refer to the buttons and group object easily object easily in Unity
    public GameObject mainMenuPanel;  // I used public variable so I can refer to the main tital & button object easily in Unity
    public GameObject optionMenuPanel; // Holds Option Menu title and buttons
    public GameObject optionMusicMenuPanel; 
    void Start()
    {
        AudioManager.Instance.PlayMusic("menu");
        // so void Start() it is a function where it calls the first frame in the game so the first things happens in the game is in the Start() function
        // I called the ShowMainMenu() function on the start() function
        ShowMainMenu(); // it will show the Start and option and quit button and a tital says Main menu 
    }

    // so I made this function StartGame() where it can be activate when you click a button (I applied this button on the Start Game button so it can load of the game)
    public void StartGame()
    {
        // play the special jingle
        AudioManager.Instance.PlaySFX("start");

        // then load your level (with a tiny delay if you like)
        SceneManager.LoadScene("level-1");
    }
    public void OnAnyOtherUIButton()
    {
        AudioManager.Instance.PlaySFX("click");
    }

    // so for ShowOption() and ShowMainMenu() functions I made two empty game object "menu" and "option" with in the menu you have the Start Game and option and quit button and a tital says "Main menu"
    // and with in the option it has option tital and a one button to return back because option settings function has not been implemented yet

    // this function is active on the option button within the menu game object 
    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false); // it will hide the menu game object
        optionMenuPanel.SetActive(true); // it will unhide/show the option game object
        optionMusicMenuPanel.SetActive(false);
    }
    // this function is active on the "back to menu" button within the option game object 
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true); // it will unhide/show the menu game object
        optionMenuPanel.SetActive(false); // it will hide the option game object
        optionMusicMenuPanel.SetActive(false) ;
    }
    public void ShowSoundOption()
    {
        mainMenuPanel.SetActive(false); // it will unhide/show the menu game object
        optionMenuPanel.SetActive(false); // it will hide the option game object
        optionMusicMenuPanel.SetActive(true);
    }
    // I used the hiding text and buttons as a method in navigating to diffrenet buttons like menu buttons and option buttons
    // because it is faster and more effecent than loading a scene for an option and loading a scene for the start menu

    // so this function is set to the quit button with in the menu game object
    public void QuitGame()
    {
        Application.Quit(); // it will close the game if its build
#if UNITY_EDITOR // if its not build it will just stop playing in unity
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#endif
    }
}
