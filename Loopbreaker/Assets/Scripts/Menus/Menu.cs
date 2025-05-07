using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OrnBoss()
    {
        SceneManager.LoadScene(2);
    }

    public void PokBoss()
    {
        SceneManager.LoadScene(3);
    }

    public void options()
    {
        SceneManager.LoadScene(5);
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
