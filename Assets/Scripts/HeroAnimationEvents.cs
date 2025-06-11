using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationEvents : MonoBehaviour
{
    public void OnAttack()
    {
        Hero.Instance.OnAttack(); // это вызывает метод из основного скрипта
    }
}
