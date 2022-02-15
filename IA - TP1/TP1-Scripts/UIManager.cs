using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite dustSprite;
    public Sprite jewelSprite;
    public Sprite bothSprite;
    public TMP_Text iterationText;
    public TMP_Text searchText;
    public TMP_Text jewelText;
    public TMP_Text dustText;
    public GridLayoutGroup perceptionMap;
    public VerticalLayoutGroup actionQueue;
    public GameObject actionUIElement;

    public void UpdateSearchText(bool informedSearch)
    {
        if (informedSearch)
        {
            searchText.text = "Informed search";
        }
        else
        {
            searchText.text = "Uninformed search";
        }
    }

    public void UpdateSelectedPerceptionUI(int index, Room room)
    {
        Transform cell = perceptionMap.transform.GetChild(index);
        Image image = cell.GetChild(0).GetComponent<Image>();
        if (room.dust != null && room.jewel != null)
        {
            image.sprite = bothSprite;
            image.color = Color.white;
        }
        else if (room.dust != null)
        {
            image.sprite = dustSprite;
            image.color = Color.white;
        }
        else if (room.jewel != null)
        {
            image.sprite = jewelSprite;
            image.color = Color.white;
        }
        else
        {
            image.sprite = null;
            image.color = Color.clear;
        }
    }

    public void UpdatePerceptionUI(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            UpdateSelectedPerceptionUI(i, rooms[i]);
        }
    }

    public void EnqueueActionUI(List<Action> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            GameObject actionText = Instantiate(actionUIElement, actionQueue.transform);
            actionText.GetComponent<TMP_Text>().text = actions[i].ToString();
        }
    }

    public void DequeueAction(int i)
    {
        Destroy(actionQueue.transform.GetChild(i).gameObject);
    }
}
