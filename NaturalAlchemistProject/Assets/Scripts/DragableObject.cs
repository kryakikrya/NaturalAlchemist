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
    [SerializeField] private Collider2D _collider;
    [SerializeField] private string _tag;
    [SerializeField] private string _trashtag;
    [SerializeField] private float ObjectPosAwayFromCamera = 7f;
    private void OnMouseDrag()
    {
        if (_collider != null)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, ObjectPosAwayFromCamera); ;
        }
    }
    private void OnMouseDown()
    {
        if (_collider != null)
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
        if (_collider != null && ! source)
        {
            _collider.enabled = false;
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(Camera.main.transform.position, direction))
            {
                if (hit.transform.CompareTag(_trashtag))
                {
                    Debug.Log("Nom nom");
                    Destroy(this.gameObject); 
                    return;
                } else if (hit.transform.CompareTag(_tag) && hit.transform.childCount <= 0) 
                {
                    parentAfterDrag = hit.transform;
                }
            }
            if (parentAfterDrag.childCount > 0) Destroy(gameObject);
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position + new Vector3(0, 0, -0.3f);
            _collider.enabled = true;
        }
    }
}
