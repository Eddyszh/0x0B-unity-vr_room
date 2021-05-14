using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private NavMeshAgent nav;

    public void OnPointerClick(PointerEventData eventData)
    {
        nav.Warp(eventData.pointerPressRaycast.worldPosition);
    }
}
