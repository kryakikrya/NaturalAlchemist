using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CropRaisingBehaivor : MonoBehaviour
{
    [SerializeField] private int maxcount = 4;
    [SerializeField] private GameObject prefab;
    [SerializeField] private SeedElement currentclass;
    bool canuse = true;
    int currentcount = 0;
    IEnumerator grownewone()
    {
        GameObject clone = Instantiate(prefab, transform);
        clone.transform.localScale /= 4;
        DragableObject dobj = clone.GetComponent<DragableObject>();
        dobj.CanGrab = false;
        clone.GetComponent<SeedClass>().element = currentclass;
        yield return new WaitForSeconds(1);
        clone.transform.localScale *= 2;
        yield return new WaitForSeconds(1);
        clone.transform.localScale *= 2;
        dobj.CanGrab = true;
    }

    private void Update()
    {
        if (transform.childCount > 0 && currentclass == null && currentcount == 0&& canuse)
        {
            currentcount = maxcount;
            Transform child = transform.GetChild(0);
            if ( child != null && child.TryGetComponent<SeedClass>(out SeedClass classa))
            {
                currentclass = classa.element;
            }
            canuse = false;
        } else if (transform.childCount == 0 && currentclass != null && currentcount > 0 && ! canuse)
        {
            currentcount--;
            Debug.Log(currentcount.ToString());
            if (currentcount == 0) { currentclass = null; return; }
            StartCoroutine(grownewone());
        } else if (transform.childCount == 0 && currentclass == null && currentcount == 0)
        {
            canuse = true;
        }
    }
}
