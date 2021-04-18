using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rule_Controller
{
  
    List<Rule> rules = new List<Rule>();
    public int rulesNumber=0;
    Rule r;
    public void AddRule(Rule r)
    {
        rules.Add(r);
        rulesNumber++;
    }
    public Rule getRuleByID(int ID)
    {
        int OK=0;
        int k=0;
        for(int i=0;i<rulesNumber;i++)
        {
            if(OK==1)
            {
                break;
            }
            if(i==ID)
            {
                k = i;
                
            }
        }
     
            return rules.ElementAt(k);
           
    }
    public void checkRule()
    {
        
        for(int i=0;i<rulesNumber;i++)
        {
            r.checkrule();
        }
    }

    public List<Rule> getList()
    {
        return rules;
    }
}
