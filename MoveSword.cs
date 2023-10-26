using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Сделать так ,чтобы при соприкосновении меч уничтожался
// Меч должен быть направлен на игрока
// Мечи должны появляться раз в несколько секунд 
// Меч должен лететь в игрока падать при попадани в сохраненную позицию ,после падать с гравитацией и
// исчезать через пару секунд 


public class MoveSword : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speedSword;
    [SerializeField] GameObject Sword;
    [SerializeField] int _maxSword;
    private Vector3 _rotation = new Vector3(0,0,0);
    private Vector3 _position = new Vector3(0,0,0);
    bool isCollisionEnter;
    List<GameObject> _sword;
    void Start()
    {
        GameObject elementSword = Instantiate(Sword, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        _sword.Add(elementSword);
        Debug.Log(_sword.Count());
    }

    void FixedUpdate()
    {
        CreateObject();
        Debug.Log(_sword.Count());
    }

    void CreateObject()
    {
        _sword.Add(Instantiate(Sword, Position(ref _position), 
            DirectionClone(ref _rotation, _sword,_sword.Count() - 1)));
        for (int i = 0; i < _sword.Count(); i++)
        {
            _sword[i].GetComponent<Transform>().rotation = DirectionClone(ref _rotation, _sword, i);
        }
        if (_sword.Count() > _maxSword)
        {
            Destroy(_sword[_sword.Count() - 1]);
        }
    }
    Vector3 Position(ref Vector3 _position)
    {
        Vector2 _randomVector2 = Random.insideUnitCircle.normalized * Random.Range(10f, 20f);
        Vector3 _randomVector3 = new Vector3(_randomVector2.x, _target.position.y - 1.5f, _randomVector2.y);
        _position = _target.position + _randomVector3;
        return _position;
    }
    Quaternion DirectionClone(ref Vector3 _rotation,List<GameObject> _sword, int i)
    {
        _rotation = _target.position - _sword[i].GetComponent<Transform>().position;
        Quaternion _direction = Quaternion.LookRotation(_rotation, Vector3.up);
        return _direction;
    }
    Quaternion Direction(ref Vector3 _rotation)
    {
        _rotation = _target.position - transform.position;
        Quaternion _direction = Quaternion.LookRotation(_rotation, Vector3.up);
        return _direction;
    }
    void GetCollisionEnter(bool value)
    {
        isCollisionEnter = value;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yes");
        GetCollisionEnter(true);
    }
}
