using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Vector3 pos;



//Проверка на поиск игрока
    private void Awake()
    {
        if (!Player)
            Player = FindObjectOfType<Hero>().transform;
    }


//Поиск игрока при помощи метода лерп 
    private void Update()
    {
        pos = Player.position;
        pos.z = -10f;
        pos.y += 3f;
        
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}