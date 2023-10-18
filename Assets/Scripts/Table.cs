using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;

    private Dictionary<string, int> results = new Dictionary<string, int>();

    internal static Table Instance;

    private void Awake()
    {
        Instance= this;
    }

    private void Start()
    {
        StartCoroutine(SetScores());
    }

    /*public void SetNewParticipant(MeatEatingComponent previous, MeatEatingComponent newParticipant)
    {
        for(int i = 0; i < participants.Count; i++)
        {
            if (participants[i] == previous)
                participants[i] = newParticipant;
        }
    }*/

    IEnumerator SetScores()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            results = new Dictionary<string, int>();

            MeatEatingComponent[] objects = FindObjectsOfType<MeatEatingComponent>();
            foreach (MeatEatingComponent obj in objects)
                results.Add(obj.GetName(), obj._meatEaten);

            var sorted = results.OrderByDescending(key => key.Value);

            int i = 0;
            foreach(KeyValuePair<string, int> obj in sorted)
            {
                if(obj.Key != "")
                    _texts[i].text = $"{i+1}. {obj.Key} - {obj.Value}";
                else
                    _texts[i].text = $"{i+1}. Вы - {obj.Value}";
                i++;
                if(i == 6)
                        break;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
