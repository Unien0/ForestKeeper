using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropDetails cropDetails;
    public TileDetails tileDetails;
    private int harvestActionCount;
    public bool CanHarvest => tileDetails.growthDays >= cropDetails.TotalGrowDays;
    private Animator anim;
    private Transform PlayerTransfrom => FindObjectOfType<Player>().transform;


    public void ProcessToolAction(ItemDetails tool,TileDetails tile)
    {
        tileDetails = tile;

        //����ʹ�ô���
        int requireActionCount = cropDetails.GetTotalRequireCount(tool.itemID);
        if (requireActionCount == -1) return;

        anim = GetComponentInChildren<Animator>();

        //���������
        if (harvestActionCount < requireActionCount)
        {
            harvestActionCount++;
            //�ж��Ƿ��ж��� ��ľ
            if (anim != null && cropDetails.hasAnimation)
            {
                if (PlayerTransfrom.position.x < transform.position.x)
                    anim.SetTrigger("RotateRight");
                else
                    anim.SetTrigger("RotateLeft");
            }
            //��������
            if(cropDetails.hasParticalEffect)
            EventHandler.CallParticleEffectEvent(cropDetails.effectType, transform.position + cropDetails.effectPos);
            //��������
        }

        if (harvestActionCount >= requireActionCount)
        {
            if (cropDetails.generateAtPlayerPosition || !cropDetails.hasAnimation)
            {
                //����ũ����
                SpawnHarvestItems();
            }
            else if(cropDetails.hasAnimation)
            {
                //���������Ķ���
                if (PlayerTransfrom.position.x < transform.position.x)
                {
                    anim.SetTrigger("FallingRight");
                }
                else
                {
                    anim.SetTrigger("FallingLeft");
                }
                StartCoroutine(HarvestAfterAnimation());
            }
        }
    }

    private IEnumerator HarvestAfterAnimation()
    {
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("END"))
        {
            yield return null;
        }
        SpawnHarvestItems();

        //ת��������
        if (cropDetails.transferItemID > 0)
        {
            CreateTransferCrop();
        }
    }

    private void CreateTransferCrop()
    {
        tileDetails.seedItemID = cropDetails.transferItemID;
        tileDetails.daysSinceLastHarvest = -1;
        tileDetails.growthDays = 0;

        EventHandler.CallRefreshCurrentMap();
    }


    /// <summary>
    /// ���ɹ�ʵ
    /// </summary>
    public void SpawnHarvestItems()
    {
        for (int i = 0; i < cropDetails.producedItemID.Length; i++)
        {
            int amountToProduce;

            if (cropDetails.producedMinAmount[i] == cropDetails.producedMaxAmount[i])
            {
                //����ֻ����ָ��������
                amountToProduce = cropDetails.producedMinAmount[i];
            }
            else    //��Ʒ�������
            {
                amountToProduce = Random.Range(cropDetails.producedMinAmount[i], cropDetails.producedMaxAmount[i] + 1);
            }

            //ִ������ָ����������Ʒ
            for (int j = 0; j < amountToProduce; j++)
            {
                if (cropDetails.generateAtPlayerPosition)
                {
                    EventHandler.CallHarvestAtPlayerPosition(cropDetails.producedItemID[i]);
                }
                else   //�����ͼ�ϵ���Ʒ
                {
                    //�ж�Ӧ�����ɵ���Ʒ����
                    var dirX = transform.position.x > PlayerTransfrom.position.x ? 1 : -1;
                    //һ����Χ�ڵ����
                    var spawnPos = new Vector3(transform.position.x + Random.Range(dirX, cropDetails.spawnRadius.x * dirX),
                    transform.position.y + Random.Range(-cropDetails.spawnRadius.y, cropDetails.spawnRadius.y), 0);

                    EventHandler.CallInstantiateItemInScene(cropDetails.producedItemID[i], spawnPos);
                }
            }
        }

        if (tileDetails != null)
        {
            tileDetails.daysSinceLastHarvest++;

            //�Ƿ�����ظ�����
            if (cropDetails.daysToRegrow > 0 && tileDetails.daysSinceLastHarvest < cropDetails.regrowTimes - 1)
            {
                tileDetails.growthDays = cropDetails.TotalGrowDays - cropDetails.daysToRegrow;
                //ˢ������
                EventHandler.CallRefreshCurrentMap();
            }
            else    //�����ظ�����
            {
                tileDetails.daysSinceLastHarvest = -1;
                tileDetails.seedItemID = -1;
                //FIXME:�Լ����
                // tileDetails.daysSinceDug = -1;
            }

            Destroy(gameObject);
        }
    }
}