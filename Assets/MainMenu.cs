using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // 1. Ссылка на компонент, который будет проигрывать звук
    public AudioSource audioSource;

    // 2. Ссылка на сам звуковой эффект (файл)
    public AudioClip buttonClickSound;


    public void PlayGame()
    {

        // 3. Проигрываем звук перед загрузкой сцены
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ExitGame()
    {
        // 3. Проигрываем звук перед выходом
        audioSource.PlayOneShot(buttonClickSound);
        
        Application.Quit();
        Debug.Log("Игра закрылась");
    }
}