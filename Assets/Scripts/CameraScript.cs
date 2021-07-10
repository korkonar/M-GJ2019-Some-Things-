using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    private string CAMERA_SHIFT;

    private bool isDebugMode = false;
    private bool zooming_in = false;
    private float shift_cooldown = 0.0f;
    private new TilemapRenderer renderer;

    public int current_scale;
    public GameObject Foreground;
    public GameObject v_cam_near;
    public GameObject v_cam_far;

    public void ShiftScale()
    {
        if (current_scale == 0)
        {
            v_cam_near.SetActive(false);
            current_scale = 1;
            shift_cooldown = 1.0f;
            renderer.material.DOFade(1.0f, 1.0f);
        }
        else
        {
            v_cam_near.SetActive(true);
            current_scale = 0;
            zooming_in = true;
            shift_cooldown = 1.0f;
            renderer.material.DOFade(0.5f, 1.0f);
        }
    }

    public void SetFollowObject(GameObject to_follow, bool switching_scale)
    {
        if (!switching_scale)
        {
            v_cam_far.GetComponent<CinemachineVirtualCamera>().Follow = to_follow.transform;
            v_cam_far.GetComponent<CinemachineVirtualCamera>().LookAt = to_follow.transform;
            v_cam_near.GetComponent<CinemachineVirtualCamera>().Follow = to_follow.transform;
            v_cam_near.GetComponent<CinemachineVirtualCamera>().LookAt = to_follow.transform;
        }
        else if (current_scale == 0)
        {
            v_cam_far.GetComponent<CinemachineVirtualCamera>().Follow = to_follow.transform;
            v_cam_far.GetComponent<CinemachineVirtualCamera>().LookAt = to_follow.transform;
        }
        else
        {
            v_cam_near.GetComponent<CinemachineVirtualCamera>().Follow = to_follow.transform;
            v_cam_near.GetComponent<CinemachineVirtualCamera>().LookAt = to_follow.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CAMERA_SHIFT = "Jump";
        renderer = Foreground.GetComponent<TilemapRenderer>();

        if (current_scale == 0)
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
        }
        else
        {
            v_cam_near.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shift_cooldown > 0)
        {
            shift_cooldown -= Time.deltaTime;
            if (shift_cooldown <= 0 && zooming_in)
            {
                zooming_in = false;
            }
        }

        if (isDebugMode && Input.GetAxis(CAMERA_SHIFT) > 0 && shift_cooldown <= 0)
        {
            ShiftScale();
        }
    }
}
