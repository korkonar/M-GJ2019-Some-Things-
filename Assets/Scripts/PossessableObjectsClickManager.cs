using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PossessableObjectsClickManager : MonoBehaviour
{
    public static bool menuOpen = false;

    private bool isInPossessMode = false;
    private bool mustEnableMovement = true;
    private bool showQImage = false;
    private float click_cooldown = 0;

    public Camera MainCamera; 
    public GameObject mask;
    public GameObject maskWrapper;
    public GameObject masky; 
    public GameObject modalPanelObject;
    public GameObject particles;
    public GameObject player;
    public GameObject QCanvasObject;
    public LayerMask layerMask;
    public AudioSource sound;

    void PossessObject(GameObject obj)
    {
        particles.transform.position = player.transform.position;
        particles.GetComponent<ParticleSystem>().Play();
        //particles.GetComponentInChildren<TrailRenderer>().emitting = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        particles.transform.DOJump(obj.transform.position, 2.0f, 4, 0.7f, false);
        player = obj;
        sound.Play();
        GameObject.Find("KongregateAPI").GetComponent<ListOfPossessedObjects>().addObj(player.GetComponent<ObjNum>().objectNum);

        mustEnableMovement = false;
    }

    void enterPossessMode()
    {
        showQImage = true;
        mask.SetActive(true);
        masky.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;

        isInPossessMode = true;
    }

    void exitPossessMode()
    {
        showQImage = false; 
        mask.SetActive(false);
        masky.SetActive(false);
        if (mustEnableMovement == true)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }

        isInPossessMode = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // player = GameObject.Find("Tree");
        maskWrapper = GameObject.Find("QSpriteMask");

        GameObject.Find("KongregateAPI").GetComponent<ListOfPossessedObjects>().addObj(player.GetComponent<ObjNum>().objectNum);

        float scale_change = player.GetComponent<ObjNum>().circle_scale;
        maskWrapper.transform.localScale = new Vector3(scale_change * 0.75f, scale_change * 0.75f, 1);
        maskWrapper.transform.DOScale(scale_change, 0.5f);
        // maskWrapper.transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        maskWrapper.transform.position = player.transform.position;

        if (Input.GetKeyDown("escape"))
        {
            modalPanelObject.SetActive(true);
            menuOpen = true;
        }

        if (click_cooldown > 0)
        {
            click_cooldown -= Time.deltaTime;
        }

        if (menuOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                enterPossessMode();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                exitPossessMode();
            }

            if (isInPossessMode && Input.GetMouseButtonDown(0) && click_cooldown <= 0)
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
                Debug.Log(maskWrapper.GetComponent<CircleCollider2D>().OverlapPoint(hit.point));
                if (hit)
                {
                    GameObject obj = hit.collider.gameObject;
                    if (maskWrapper.GetComponent<CircleCollider2D>().OverlapPoint(hit.point))
                    {
                        if (obj.tag == "bigPossessable" || obj.tag == "smallPossessable")
                        {
                            mustEnableMovement = false;
                            float scale_change = obj.GetComponent<ObjNum>().circle_scale;
                            maskWrapper.transform.localScale = new Vector3(scale_change * 0.75f, scale_change * 0.75f, 1);
                            maskWrapper.transform.DOScale(scale_change, 0.5f);
                            if (player.tag == "bigPossessable" && obj.tag == "smallPossessable")
                            {

                                MainCamera.GetComponent<CameraScript>().SetFollowObject(particles, true);
                                float d = Vector2.Distance(player.transform.position, obj.transform.position);
                                //StartCoroutine(ExecuteAfterTime(0.12f * d));
                                StartCoroutine(ExecuteAfterTime(2));
                                MainCamera.GetComponent<CameraScript>().ShiftScale();
                            }
                            else if (player.tag == "smallPossessable" && obj.tag == "bigPossessable")
                            {
                                MainCamera.GetComponent<CameraScript>().SetFollowObject(particles, true);
                                float d = Vector2.Distance(player.transform.position, obj.transform.position);
                                //StartCoroutine(ExecuteAfterTime(0.12f * d));
                                StartCoroutine(ExecuteAfterTime(2));
                                MainCamera.GetComponent<CameraScript>().ShiftScale();
                            }
                            else
                            {
                                MainCamera.GetComponent<CameraScript>().SetFollowObject(particles, false);
                                float d = Vector2.Distance(player.transform.position, obj.transform.position);
                                //StartCoroutine(ExecuteAfterTime(0.09f * d));
                                StartCoroutine(ExecuteAfterTime(2));
                            }
                            exitPossessMode();
                            PossessObject(obj);
                            click_cooldown = 2.0f;
                        }
                    }
                }
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        MainCamera.GetComponent<CameraScript>().SetFollowObject(player, false);
        //particles.GetComponentInChildren<TrailRenderer>().emitting = false;
        player.GetComponent<PlayerMovement>().enabled = true;
        mustEnableMovement = true;
    }
}
