using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public string[] dialogLines; // реплики
    private int currentLine = 0;
    private bool isPlayerNear = false;
    private bool isDialogActive = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogActive)
            {
                isDialogActive = true;
                currentLine = 0;
                Debug.Log(dialogLines[currentLine]);
            }
            else
            {
                currentLine++;
                if (currentLine < dialogLines.Length)
                {
                    Debug.Log(dialogLines[currentLine]);
                }
                else
                {
                    Debug.Log("Конец диалога");
                    isDialogActive = false;
                    currentLine = 0;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Нажми E, чтобы поговорить");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            isDialogActive = false;
            currentLine = 0;
            Debug.Log("Вы отошли от NPC");
        }
    }
}