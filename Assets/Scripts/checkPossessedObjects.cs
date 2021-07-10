using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkPossessedObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int tot=0;
        int i = 0;
        foreach (int o in GameObject.Find("KongregateAPI").GetComponent<ListOfPossessedObjects>().possessedObjs)
        {
            if (o == 1)
            {
                gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                tot++;
            }
            i++;
        }
        GameObject.Find("KongregateAPI").GetComponent<KongregateAPIBehaviour>().SendTotItems(tot);
        if(tot==14){
            GameObject.Find("KongregateAPI").GetComponent<KongregateAPIBehaviour>().SendAllItems();
        }
    }
}
