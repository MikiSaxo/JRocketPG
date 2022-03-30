using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject objet;

    public void OnPointerEnter(PointerEventData eventData)
    {
        objet.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objet.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(1);
    }
}