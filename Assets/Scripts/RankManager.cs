using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform[] challengers;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] float rankCheckDelay;

    Dictionary<string, Transform> players = new Dictionary<string, Transform>();

    void Start()
    {
        foreach (Transform player in challengers)
        {
            players.Add(player.name, player);
        }

        StartCoroutine(nameof(CheckRanking));
    }

    IEnumerator CheckRanking()
    {
        while (GameManager.Instance.IsGameActive)
        {
            foreach (Transform player in challengers)
            {
                SetRank(player);
            }

            yield return new WaitForSeconds(rankCheckDelay);
        }
    }

    void SetRank(Transform player)
    {
        players[player.name] = player;

        IOrderedEnumerable<KeyValuePair<string, Transform>> sortedPlayer =
                players.OrderByDescending(x => x.Value.transform.position.z);


        int i = 0;

        foreach (KeyValuePair<string, Transform> item in sortedPlayer)
        {
            if (item.Value.CompareTag("Player"))
            {
                rankText.text = (i + 1).ToString();

                //TO SHOW ALL OPPONENTS IN RANKING LIST, REPLACE ABOVE LINE WITH THIS AND ADD TEXT ARRAY
                //rankTexts[i].text = (i + 1) + " . " + item.Value.name;
            }

            i++;
        }
    }
}
