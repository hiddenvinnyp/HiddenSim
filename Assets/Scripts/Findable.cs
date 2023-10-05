using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(OutlineMesh))]
[RequireComponent(typeof(UniqueID))]
[RequireComponent(typeof(AudioSource))]
public class Findable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<string> ItemSelected;
    public HiddenItem Prefs;
    public bool IsFound;

    private string _id;
    private OutlineMesh _outline;
    private AudioSource _pickUpSFX;

    private void Start()
    {
        _pickUpSFX = GetComponent<AudioSource>();
        _outline = GetComponent<OutlineMesh>();
        _outline.enabled = false;
        _id = GetComponent<UniqueID>().Id;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // �����, ����� ����� ��������, �������� �������� ������� ��������
        // ������� �������, �������� ��������� � UI ������
        Debug.Log("Click on " + Prefs.Name);
        // IsFound = IsItemInList? true : false
        _pickUpSFX.Play();
        ItemSelected?.Invoke(_id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _outline.enabled = false;
    }
}