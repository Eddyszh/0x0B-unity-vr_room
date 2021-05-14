using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    private PointerEventData _eventData;

    private void Start()
    {
        _eventData = new PointerEventData(EventSystem.current);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        if (Physics.Raycast(transform.position, transform.forward, out var hit, _maxDistance))
        {
            if (_gazedAtObject != hit.transform.gameObject)
            {
                //New GameObject.
                if (_gazedAtObject)
                    _gazedAtObject.GetComponentInParent<IPointerExitHandler>()?.OnPointerExit(_eventData);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.GetComponentInParent<IPointerEnterHandler>()?.OnPointerEnter(_eventData);
            }
        }
        else if (_gazedAtObject)
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject.GetComponentInParent<IPointerExitHandler>()?.OnPointerExit(_eventData);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (_gazedAtObject != null && Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
        {
            var clicked = _gazedAtObject.GetComponentInParent<IPointerClickHandler>();
            if (clicked != null)
                _eventData.pointerPressRaycast = new RaycastResult { worldPosition = hit.point };
            clicked?.OnPointerClick(_eventData);
            //_gazedAtObject.GetComponentInParent<IPointerClickHandler>()?.OnPointerClick(_eventData);
            Debug.Log(_gazedAtObject);
        }
    }
}
