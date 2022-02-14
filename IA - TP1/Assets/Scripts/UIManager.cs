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


    public void UpdatePerceptionUI(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            Transform cell = perceptionMap.transform.GetChild(i);
            Image image = cell.GetChild(0).GetComponent<Image>();
            if (rooms[i].dust != null && rooms[i].jewel != null)
            {
                image.sprite = bothSprite;
                image.color = Color.white;
            }
            else if (rooms[i].dust != null)
            {
                image.sprite = dustSprite;
                image.color = Color.white;
            }
            else if (rooms[i].jewel != null)
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
