using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public LayerMask boxesLayer;
    private int achieved = 0;
    public int goals;
    public int currLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(achieved == goals)
        {
            nextLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Boxes"))
        {
            Debug.Log(collision);
            achieved++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Boxes"))
        {
            achieved--;
        }
    }

    public void nextLevel()
    {
        if(currLevel == 1)
        {
            SceneManager.LoadSceneAsync("Level 2");
        }
        if(currLevel == 2)
        {
            SceneManager.LoadSceneAsync("Level 3");
        }
        if(currLevel == 3)
        {
            SceneManager.LoadSceneAsync("Victory");
        }  
    }

}
