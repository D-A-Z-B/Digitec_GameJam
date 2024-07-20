using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public enum UXML {
    Upgrade
}

[CreateAssetMenu(menuName = "SO/UI/UXMLHelper")]
public class UXMLHelperSO : ScriptableObject
{
    public VisualTreeAsset UpgradeAsset;

    private Dictionary<UXML, VisualTreeAsset> _treeDictionary;

    private void OnEnable() {
        _treeDictionary = new Dictionary<UXML, VisualTreeAsset>();

        foreach(UXML uxml in Enum.GetValues(typeof(UXML))) {
            string uxmlStr = uxml.ToString();
            Type t = GetType();
            FieldInfo fieldInfo = t.GetField($"{uxmlStr}Asset");
            
            var tree = fieldInfo.GetValue(this) as VisualTreeAsset;
            _treeDictionary.Add(uxml, tree);
        }
    }

    public VisualTreeAsset GetTree(UXML uxml) {
        if(_treeDictionary.ContainsKey(uxml)) {
            return _treeDictionary[uxml];
        }
        return null;
    }
}
