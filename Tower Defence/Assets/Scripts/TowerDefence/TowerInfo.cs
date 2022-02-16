using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerInfo : MonoBehaviour
{
    public string towerName;
    public GameObject TowerPrefab;

    public int cost;
    public TextMeshProUGUI costText;

    public Image iconComp;
    public Sprite prefabIcon;
}
