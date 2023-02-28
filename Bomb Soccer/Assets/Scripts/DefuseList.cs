using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefuseList : MonoBehaviour
{
    public int defuseCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        defuseCount = transform.childCount;
    }

    public void Win()
    {
        for(int i = 0; i < defuseCount; i++)
        {
            Defuse curr = transform.GetChild(i).GetComponent<Defuse>();
            curr.WinLevel();
        }
    }
}
