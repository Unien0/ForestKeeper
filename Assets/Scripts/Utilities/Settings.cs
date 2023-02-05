using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public const float fadeDuration = 0.35f;
    public const float targetAlpha = 0.45f;

    //time
    public const float secondThreshold = 0.01f;//越小越快
    public const int secondHold = 59;
    public const int minuteHold = 59;
    public const int hourHold = 23;
    public const int dayHold = 30;
    public const int seasonHold = 3;

    //Transition
    public const float UIFadeDuration = 0.5f;

    //割草数量限制
    public const int reapAmount = 2;
    //NPC网格移动
    public const float gridCellSize = 1;
    public const float gridCellDiagonalSize = 1.41f;
    //单位像素距离
    public const float pixelSize = 0.05f;
    //动画间隔
    public const float animationBreakTime = 5f;
    //最大的网格尺寸
    public const int maxGridSize = 9999;
}
