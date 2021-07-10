using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfPossessedObjects : MonoBehaviour
{
    public enum PObjects
    {
        Bus,
        Bush,
        Character,
        Chest,
        Coin,
        Duck,
        Flower,
        Flower2,
        Gnome,
        Motorbike,
        Rock,
        Spiky,
        Tree,
        Whale,
        Dino

    };

    public int[] possessedObjs;

    public void addObj(PObjects ob)
    {
        if( possessedObjs[(int)ob]==0){
            int a=PlayerPrefs.GetInt("TotalItemsPossessed",0);
            PlayerPrefs.SetInt("TotalItemsPossessed",++a);
        }
        PlayerPrefs.SetInt(ob.ToString(),1);
        possessedObjs[(int)ob] = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        possessedObjs = new int[(int)PObjects.Dino + 1];
        
        possessedObjs[0]= PlayerPrefs.GetInt("Bus",0);
        possessedObjs[1]= PlayerPrefs.GetInt("Bush",0);
        possessedObjs[2]= PlayerPrefs.GetInt("Character",0);
        possessedObjs[3]= PlayerPrefs.GetInt("Chest",0);
        possessedObjs[4]= PlayerPrefs.GetInt("Coin",0);
        possessedObjs[5]= PlayerPrefs.GetInt("Duck",0);
        possessedObjs[6]= PlayerPrefs.GetInt("Flower",0);
        possessedObjs[7]= PlayerPrefs.GetInt("Flower2",0);
        possessedObjs[8]= PlayerPrefs.GetInt("Gnome",0);
        possessedObjs[9]= PlayerPrefs.GetInt("Motorbike",0);
        possessedObjs[10]= PlayerPrefs.GetInt("Rock",0);
        possessedObjs[11]= PlayerPrefs.GetInt("Spiky",0);
        possessedObjs[12]= PlayerPrefs.GetInt("Tree",0);    
        possessedObjs[13]= PlayerPrefs.GetInt("Whale",0);
        possessedObjs[14]= PlayerPrefs.GetInt("Dino",0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
