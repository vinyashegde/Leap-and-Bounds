using UnityEngine;
using UnityEngine.UI;

public class TriggerObject : MonoBehaviour
{
    public Text winText; 

    private void Start()
    {
        
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            YourFunction();
        }
    }

    private void YourFunction()
    {
        
        winText.gameObject.SetActive(true);
        winText.text = "You won!";
        
    }
}
