using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public RectTransform dayNightImage;
    public RectTransform clockParent;
    public Image seasonImage;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public Sprite[] seasonSprites;

    private List<GameObject> clockBlocks = new List<GameObject>();


    private void Awake()
    {
        for (int i = 0; i < clockParent.childCount; i++)
        {
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
        EventHandler.GameDataEvent += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
        EventHandler.GameDataEvent -= OnGameDateEvent;
    }
    private void OnGameMinuteEvent(int minute, int hour,int day ,Season season)
    {
        timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    }

    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        dateText.text = year + "/" + month.ToString("00") + "/" + day.ToString("00");
        seasonImage.sprite = seasonSprites[(int)season];

        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }

    /// <summary>
    /// 根据小时切换时间块显示
    /// </summary>
    /// <param name="hour"></param>
    private void SwitchHourImage(int hour)
    {
        int index = hour;

        if (index % 6 == 0)
        {
            foreach (var item in clockBlocks)
            {
                item.SetActive(false);
            }
            clockBlocks[0].SetActive(true);
        }
        else
        {
            for (int i = 0; i < clockBlocks.Count; i++)
            {
                int num = index % 6;
                if (i < num + 1)
                    clockBlocks[i].SetActive(true);
                else
                    clockBlocks[i].SetActive(false);
            }
        }
    }

    private void DayNightImageRotate(int hour)
    {
        int Findex = hour / 6;
        for (int i = 0; i < Findex; i++)
        {
            var target = new Vector3(0, 0, 90);
            dayNightImage.DORotate(target, 1f, RotateMode.Fast);
        }

        //var target = new Vector3(0, 0, hour * 15 - 90);
        //dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }

}
