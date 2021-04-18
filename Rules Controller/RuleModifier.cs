using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class RuleModifier : Rule_Controller
{
    
   public void RuleFromFile(List<string> l)
    {
        System.IO.StreamReader sr = new System.IO.StreamReader("rules.txt");
        while (!sr.EndOfStream)
        {
            l.Add(sr.ReadLine());
        }
        
        sr.Close();
    }

}
