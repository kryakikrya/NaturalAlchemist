using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragableObject : MonoBehaviour
{
    [HideInInspector] internal Transform parentAfterDrag;
    [SerializeField] internal bool source;
    [SerializeField] internal bool CanGrab = true;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private string _tag;
    [SerializeField] private string _trashtag;
    [SerializeField] private float ObjectPosAwayFromCamera = 7f;
    private void OnMouseDrag()
    {
        if (_collider != null && CanGrab)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, ObjectPosAwayFromCamera); ;
        }
    }
    private void OnMouseDown()
    {
        if (_collider != null && CanGrab)
        {
            parentAfterDrag = transform.parent;
            if (source)
            {
                GameObject sourceobjet = Instantiate(gameObject, parentAfterDrag);
                string name = gameObject.name;
                gameObject.name = name+"-CLONED";
                sourceobjet.name = name;
                source = false;
            }
            transform.SetParent(transform.root);
        }
    }
    private void OnMouseUp()
    {
        if (_collider != null && ! source && CanGrab)
        {
            _collider.enabled = false;
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ObjectPosAwayFromCamera));
            Vector3 direction = Vector3.down;
            direction.Normalize();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, direction, 10000f, layerMask: 1);
            if (hit)
            {
                if (hit.transform.CompareTag(_trashtag))
                {
                    Destroy(this.gameObject); 
                    return;
                } else if (( hit.transform.CompareTag(_tag) && hit.transform.childCount == 0)||hit.transform.CompareTag("BoilerSlot") || hit.transform.CompareTag(_trashtag) && hit.transform.childCount <= 0) 
                {
                    parentAfterDrag = hit.transform;
                    if (hit.transform.CompareTag(_trashtag))
                    {
                        Destroy(this.gameObject); 
                        return;
                    }
                }
            }
            if (parentAfterDrag.childCount > 0 && parentAfterDrag.CompareTag(_tag)) Destroy(gameObject);
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position + new Vector3(0, 0, -0.3f);
            _collider.enabled = true;
        }
    }
}
