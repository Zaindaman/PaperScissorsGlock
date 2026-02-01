using JetBrains.Annotations;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{

    [Header("Common Attack Variables")]

    public KeyCode attackKey;
    public GameObject weaponEmpty;
    public LayerMask playerLayer;

    private bool initialized = false;


    // sets up values to be used and assigned by characterwitcher
    public void Initialize(KeyCode key, GameObject weapon, LayerMask layer)
    {
        attackKey = key;
        weaponEmpty = weapon;
        playerLayer = layer;
        initialized = true;
    }


    //creates virtual function which is empty, ready for overide
    public virtual void Attack() { }


    // It just calls the virtual function
    void LateUpdate()
    {

        if (!initialized) return;


        if (Input.GetKeyDown(attackKey))
        {
            Attack();
               
        }
    }
}
