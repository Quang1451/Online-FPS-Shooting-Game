using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private DropItemContainer dropItemPrefab;

    [SerializeField] private List<Gun> WeaponPrefabs;

    private SpawnObejct<DropItemContainer> _spawnDropItem;

    private Dictionary<string, Gun> _gunDictionary;
    private Dictionary<string, SpawnObejct<Gun>> _gunPoolDictionary;
    private void Awake()
    {
        _spawnDropItem = new SpawnObejct<DropItemContainer>();
        _spawnDropItem.Initalize(dropItemPrefab);

        _gunDictionary = new Dictionary<string, Gun>();
        _gunPoolDictionary = new Dictionary<string, SpawnObejct<Gun>>();

        foreach (Gun gun in WeaponPrefabs)
        {
            _gunDictionary[gun.name] = gun;
            _gunPoolDictionary[gun.name] = new SpawnObejct<Gun>();
            _gunPoolDictionary[gun.name].Initalize(gun);
        }
    }

    [Button]
    private void Test(string gunName)
    {
        if (!_gunPoolDictionary.ContainsKey(gunName) || _gunPoolDictionary == null) return;

        DropItemContainer dropItem = _spawnDropItem.GetObject();

        Gun gun = _gunPoolDictionary[gunName].GetObject();

        dropItem.SetData(gun);
    }
}
