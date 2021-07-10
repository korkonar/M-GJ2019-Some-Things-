using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    public GameObject player;
    public PossessableObjectsClickManager manager;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoxCollider2D>().OverlapPoint(manager.player.transform.position))
        {
            sound.Play();
            SceneManager.LoadScene(0);
        }
    }
}
