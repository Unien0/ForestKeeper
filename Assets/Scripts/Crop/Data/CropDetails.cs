using UnityEngine;
[System.Serializable]

public class CropDetails 
{
    public int seedItemID;
    [Header("不同阶段的日期")]
    public int[] growthDays;
    public int TotalGrowDays
    {
        get
        {
            int amount = 0;
            foreach (var days in growthDays)
            {
                amount += days;
            }
            return amount;
        }
    }

    [Header("不同阶段的Prefab")]
    public GameObject[] growthPrefabs;

    [Header("不同阶段的图片")]
    public Sprite[] growthSprites;

    [Header("可种植的季节")]
    public Season[] season;

    [Space]
    [Header("收割工具")]
    public int[] harvestToolItemID;

    [Header("工具使用次数")]
    public int[] requireActionCount;

    [Header("转换新物体ID")]
    public int transferItemID;

    [Space]
    [Header("收割果实信息")]
    public int[] producedItemID;
    public int[] producedMinAmount;
    public int[] producedMaxAmount;
    public Vector2 spawnRadius;

    [Header("再次生长时间")]
    public int daysToRegrow;
    public int regrowTimes;

    [Header("Options")]
    public bool generateAtPlayerPosition;
    public bool hasAnimation;
    public bool hasParticalEffect;
    //TODO:特效 音效 等
    public ParticleEffectType effectType;
    public Vector3 effectPos;

    /// <summary>
    /// 检查道具当前是否可用
    /// </summary>
    /// <param name="toolID"></param>
    /// <returns></returns>
    public bool CheckToolAvailable(int toolID)
    {
        foreach (var tool in harvestToolItemID)
        {
            if (tool == toolID)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 获得工具需要使用的次数
    /// </summary>
    /// <param name="toolID">工具ID</param>
    /// <returns></returns>
    public int GetTotalRequireCount(int toolID)
    {
        for (int i = 0; i < harvestToolItemID.Length; i++)
        {
            if (harvestToolItemID[i] == toolID)
                return requireActionCount[i];
        }
        return -1;
    }
}
