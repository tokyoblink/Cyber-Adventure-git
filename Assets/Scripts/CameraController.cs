using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float smoothSpeed = 10f;
    private Vector3 pos;

    //Проверка на поиск игрока
    private void Awake()
    {
        if (!Player)
        {
            var heroObject = FindFirstObjectByType<Hero>();
            if (heroObject != null)
                Player = heroObject.transform;
        }
    }


//Поиск игрока при помощи метода лерп 
    private void Update()
    {
        if (Player == null)
        {
            Debug.LogWarning("CameraController: Player not assigned or not found!");
            return;
        }

        pos = Player.position;
        pos.z = -10f;
        pos.y += 3f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}