using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _radius = 3;
    [SerializeField] Vector3 _pos_camera = new Vector3(0, 0, 0);
    [SerializeField] double _camera_pos_x = 0;
    [SerializeField] double _camera_pos_y = 0;
    [SerializeField] double _camera_pos_z = 0;
    [SerializeField] double _sensivity = 0.2;
    [SerializeField] float _speed_camera = 10f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _pos_camera, Time.deltaTime * _speed_camera);
        Pos_camera();
        Rotate_camera();
    }

    void Rotate_camera()
    {
        Vector3 a = _target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(a, Vector3.up);
    }
    void Pos_camera()
    {
        double _sin_mouse_x = Math.Sin(-Input.mousePosition.x * _sensivity);
        double _cos_mouse_x = Math.Cos(-Input.mousePosition.x * _sensivity);
        double _sin_mouse_y = Math.Sin(Input.mousePosition.y * _sensivity);
        double _cos_mouse_y = Math.Cos(-Input.mousePosition.y * _sensivity);
        _camera_pos_y = (_radius * _sin_mouse_y + _target.position.y);
        double _radius_shtrih = _radius * (1 - _cos_mouse_y);
        _camera_pos_z =(_radius * _sin_mouse_x
            - _radius_shtrih * _sin_mouse_x + _target.position.z);
        _camera_pos_x =(_radius * _cos_mouse_x
            - _radius_shtrih * _cos_mouse_x + _target.position.x);
        
        _pos_camera = new Vector3(
            (float)_camera_pos_x, 
            Math.Clamp(-(float)_camera_pos_y, 0.5f, 200), 
            (float)_camera_pos_z);
    }
}
