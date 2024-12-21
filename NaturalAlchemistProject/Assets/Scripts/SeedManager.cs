using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{
    [Header("Сетап")]
    [SerializeField] private Transform AlchemyTransform;
    [SerializeField] private List<SeedElement> AvailableElements = new();
    [SerializeField] private Button CombineButton;
    [SerializeField] private GameObject SeedBasePrefab;
    private SeedElement GetElementFromTransform(Transform _transform)
    {
        SeedElement output = null;
        if (_transform != null && _transform.childCount > 0)
        {
            if (transform.childCount == 1)
            {
                Transform child = _transform.GetChild(0);
                if (child != null&&child.TryGetComponent<SeedClass>(out SeedClass clas)) 
                {
                    output = clas.element;
                }

            } else
            {
                foreach (SeedElement element in AvailableElements)
                {
                    int counted = 0;
                    int needed = element.ComponentElements.Count;
                    foreach (Transform child in _transform)
                    {
                        if (child != null && child.TryGetComponent<SeedClass>(out SeedClass clas) && element.ComponentElements.Contains(clas.element))
                        {
                            counted++;
                            if (counted == needed)
                            {
                                output = element;

                                return output;
                            }
                        }
                    }
                }
            }
        }
        return output;
    }
    private void OnButtonClick()
    {
        if (AlchemyTransform.childCount > 1)
        {
            SeedElement element = GetElementFromTransform(AlchemyTransform);
            if (element != null) 
            {
                for (int i = 0; i < element.ComponentElements.Count; i++) 
                {
                    Transform child = AlchemyTransform.GetChild(i);
                    if (child != null && child.TryGetComponent<SeedClass>(out SeedClass component) && element.ComponentElements.Contains(component.element))
                    {
                        Destroy(child.gameObject);
                    }
                }
                GameObject newobject = Instantiate(SeedBasePrefab, AlchemyTransform);
                SeedClass seedClass = newobject.GetComponent<SeedClass>() ?? null;
                if (seedClass == null )
                {
                    Destroy(newobject);
                    Debug.Log("Mixing failed!");
                    return;
                }
                newobject.name = element.name;
                seedClass.element = element;
            }
        }
    }
    private void Start()
    {
        CombineButton.onClick.AddListener(OnButtonClick);
    }
}
