using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "UIReferenceSO", menuName = "UI/UIReferenceSO")]
public class UIReferenceSO : ScriptableObject
{
    [TableList] public List<UIReference> references;

    private Dictionary<string, UIReference> _dictionary = new Dictionary<string, UIReference>();

    public void Initialize()
    {
        _dictionary.Clear();
        foreach (var t in references)
        {
            _dictionary[t.key] = t;
        }
    }

    public AssetReference GetAddress(string k)
    {
        UIReference uIReference = GetUIReference(k);
        return uIReference != null ? uIReference.assetRef : null;
    }

    public UIReference GetUIReference(string k)
    {
        if (!_dictionary.ContainsKey(k))
        {
            foreach (var t in references)
            {
                if (k.Equals(t.key))
                {
                    _dictionary[k] = t;
                }
            }
        }
        return _dictionary.ContainsKey(k) ? _dictionary[k] : null;
    }

    [Button]
    private void GenerateKey()
    {
        GenerateKeyCode.Generate(references.Select(x => x.key).ToList(), "UIKey");
    }
}

[Serializable]
public class UIReference
{
    public string key;
    public bool isSingle;
    public AssetReference assetRef;
}

