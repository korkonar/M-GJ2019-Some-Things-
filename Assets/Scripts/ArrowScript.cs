using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class ArrowScript : MonoBehaviour
{
    private bool show_arrow = true;
    private float max_scale;
    private Transform possessed_object;
    private Vector3 facing_direction = new Vector3(1, 0, 0);

    public Camera MainCamera;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        max_scale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (show_arrow && MainCamera.WorldToViewportPoint(Target.transform.position).x > 0 && MainCamera.WorldToViewportPoint(Target.transform.position).x <= 1 && MainCamera.WorldToViewportPoint(Target.transform.position).y > 0 && MainCamera.WorldToViewportPoint(Target.transform.position).y <= 1)
        {
            show_arrow = false;
            gameObject.transform.DOScale(0.0f, 1.0f);
        }

        if (!show_arrow && (MainCamera.WorldToViewportPoint(Target.transform.position).x <= 0 || MainCamera.WorldToViewportPoint(Target.transform.position).x > 1) || (MainCamera.WorldToViewportPoint(Target.transform.position).y <= 0 || MainCamera.WorldToViewportPoint(Target.transform.position).y > 1))
        {
            show_arrow = true;
            gameObject.transform.DOScale(max_scale, 1.0f);
        }

        if (MainCamera.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
        {
            possessed_object = MainCamera.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera.LookAt;
            Vector3 point_direction = Target.transform.position - possessed_object.position;
            float angle = Vector3.SignedAngle(point_direction, facing_direction, new Vector3(0, 0, 1));
            gameObject.transform.DORotate(new Vector3(0, 0, -angle), 0.1f);
        }
    }
}
