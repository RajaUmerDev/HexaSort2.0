using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector3 mousePosition;
    public float raycastDistance = 1f;
    [SerializeField] private Material _highlightedMaterial;
    private GameObject _lastHitObject;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Grid _dependencyOFGrid;
    private IGrid _gridInterface;

    private void Start()
    {
        _gridInterface = _dependencyOFGrid;
    }


    public void SetDependency(IGrid gridInterface)
    {
        _gridInterface = gridInterface;
    }

    void Update()
    {
        CastRayOnTile();
    }
    
    private void CastRayOnTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance))
        {
            GameObject newHitObject = hit.transform.gameObject;
            if (newHitObject != _lastHitObject)
            {
                // Reset the material of the previous hit object to its default
                if (_lastHitObject != null)
                {
                    _lastHitObject.GetComponent<MeshRenderer>().material = _defaultMaterial;
                }

                // Store the new hit object and its default material
                _lastHitObject = newHitObject;
                _defaultMaterial = _lastHitObject.GetComponent<MeshRenderer>().material;
            }

            // Change the material of the current hit object to the highlighted material
            _lastHitObject.GetComponent<MeshRenderer>().material = _highlightedMaterial;

            // Perform actions based on the detected object
            // Debug.Log("Object below chip: " + _lastHitObject.name);
        }
        else
        {
            // Reset the material of the last hit object to its default if no object is detected
            if (_lastHitObject != null)
            {
                _lastHitObject.GetComponent<MeshRenderer>().material = _defaultMaterial;
                _lastHitObject = null; // Reset _lastHitObject
            }
            // If no object is detected
            // Debug.Log("No object below chip.");
        }
    }
    
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        if (_lastHitObject != null)
        {
            var fixPos = _lastHitObject.transform.position;
            fixPos.y += 0.2f;
            gameObject.transform.position = fixPos;
            _gridInterface.AddSelectedObjectInGrid(_lastHitObject,transform.gameObject);
            Destroy(gameObject.GetComponent<Tile>());
        }
    }
}
