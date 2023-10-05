using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private DropItemContainer dropItemPrefab;

    [SerializeField] private List<Gun> WeaponPrefabs;

    public static SpawnObejct<DropItemContainer> SpawnDropItem;

    public static Dictionary<string, Gun> GunDictionary;
    public static Dictionary<string, SpawnObejct<Gun>> GunPoolDictionary;
    private void Awake()
    {
        SpawnDropItem = new SpawnObejct<DropItemContainer>();
        SpawnDropItem.Initalize(dropItemPrefab);

        GunDictionary = new Dictionary<string, Gun>();
        GunPoolDictionary = new Dictionary<string, SpawnObejct<Gun>>();

        foreach (Gun gun in WeaponPrefabs)
        {
            GunDictionary[gun.name] = gun;
            GunPoolDictionary[gun.name] = new SpawnObejct<Gun>();
            GunPoolDictionary[gun.name].Initalize(gun);
        }
    }

    [Button]
    private void Test(string gunName)
    {
        if (!GunPoolDictionary.ContainsKey(gunName) || GunPoolDictionary == null) return;

        DropItemContainer dropItem = SpawnDropItem.GetObject();

        Gun gun = GunPoolDictionary[gunName].GetObject();

        dropItem.SetData(gun);
    }

    public static Gun GetGun(string gunName)
    {
        if (!GunPoolDictionary.ContainsKey(gunName) || GunPoolDictionary == null) return null;
        return GunPoolDictionary[gunName].GetObject();
    }
}
