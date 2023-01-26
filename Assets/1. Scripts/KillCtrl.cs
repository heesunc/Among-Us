using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class KillCtrl : MonoBehaviour
{
    public Transform[] spawnPoints;

    List<int> number = new List<int>();
    // 초기화
    public void KillReset()
    {
        number.Clear(); // 비워주기

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].childCount != 0)
            {
                Destroy(spawnPoints[i].GetChild(0).gameObject);
            }
        }
        NPCSpawn();
    }
    
    // NPC 스폰
    public void NPCSpawn()
    {
        int rand = Random.Range(0, 10);

        for (int i = 0; i < 5;)
        {
            // 중복되었다면
            if (number.Contains(rand))
            {
                rand = Random.Range(0, 10);
            }
            // 중복되지 않았다면
            else
            {
                number.Add(rand);
                i++;
            }
        }
        // 스폰
        for (int i = 0; i < number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), spawnPoints[number[i]]);
        }
    }
}
