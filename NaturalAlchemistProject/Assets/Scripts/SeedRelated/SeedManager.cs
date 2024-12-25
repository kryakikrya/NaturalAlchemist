using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform AlchemyTransform;
    [SerializeField] private List<PotionClass> AvailableElements = new();
    [SerializeField] private Button CombineButton;
    [SerializeField] private GameObject SeedBasePrefab;
    [SerializeField] private AudioSource PoisonAudio;
    private PotionClass GetElementFromTransform(Transform _transform)
    {
        PotionClass output = null;
        if (_transform != null && _transform.childCount > 0)
        {
            if (transform.childCount == 1)
            {
                Transform child = _transform.GetChild(0);
                if (child != null&&child.TryGetComponent<SeedClass>(out SeedClass clas)) 
                {
                    output = clas.PotionElement;
                }

            } else
            {
                foreach (PotionClass element in AvailableElements)
                {
                    int counted = 0;
                    int needed = element.ComponentElement.Count;
                    foreach (Transform child in _transform)
                    {
                        if (child != null && child.TryGetComponent<SeedClass>(out SeedClass clas) && element.ComponentElement.Contains(clas.SeedelEment))
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
            PotionClass element = GetElementFromTransform(AlchemyTransform);
            if (element != null) 
            {
                for (int i = 0; i < element.ComponentElement.Count; i++) 
                {
                    Transform child = AlchemyTransform.GetChild(i);
                    if (child != null && child.TryGetComponent<SeedClass>(out SeedClass component) && element.ComponentElement.Contains(component.SeedelEment))
                    {
                        Destroy(child.gameObject);
                    }
                }
                GameObject newobject = Instantiate(SeedBasePrefab, AlchemyTransform);
                SeedClass seedClass = newobject.GetComponent<SeedClass>() ?? null;
                PoisonAudio.Play();
                if (seedClass == null )
                {
                    Destroy(newobject);
                    Debug.Log("Mixing failed!");
                    return;
                }
                newobject.name = element.name;
                seedClass.PotionElement = element;
            }
        }
    }
    private void Start()
    {
        CombineButton.onClick.AddListener(OnButtonClick);
    }
}
