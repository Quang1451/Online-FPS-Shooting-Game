using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private PlayerPickUpSO PickUpSO;

    private List<GameObject> _pickUpList;

    private void Awake()
    {
        _pickUpList = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(PickUpSO.IsPickUpLayer(other.gameObject.layer))
        {
            _pickUpList.Add(other.gameObject);
            Debug.Log("Pickup " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_pickUpList.Contains(other.gameObject))
        {
            _pickUpList.Remove(other.gameObject);
        }
    }
}
