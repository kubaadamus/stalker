using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class statyczne : MonoBehaviour
{
    public static void wypiszZawartosc(Item[] tabela)
    {
        foreach (Item a in tabela)
        {
            if (a == null)
            {
                Debug.Log("puste");
            }
            else
            {
                Debug.Log(a.gameObject.name);
            }

        }
    }
    public static void wypiszZawartosc(List<Item> lista)
    {
        foreach (Item a in lista)
        {
            if (a == null)
            {
                Debug.Log("puste");
            }
            else
            {
                Debug.Log(a.gameObject.name);
            }
        }
    }
}

