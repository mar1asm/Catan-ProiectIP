using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberDisplayBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private ItemInteractionHolderBehaviour holderBehaviour;

    
    public List<ResourceTypes> resourcesGiven = new List<ResourceTypes>();

    
    private bool userGaveResources = false;
    void Start()
    {
       
    }


    public void SetUserGaveResources() {
        userGaveResources = true;
    }

    public IEnumerator WaitForUserToGiveResources() {
        resourcesGiven.Clear();
        userGaveResources = false;


        Player player = playerManager.clientPlayer;
        //Player player = test;

      
        int halfRoundUp = Mathf.CeilToInt((float)(player.GetNumberOfResources()) / 2f);

        holderBehaviour.SetDisplay(player, halfRoundUp);

        yield return new WaitUntil(() => userGaveResources);

        resourcesGiven = holderBehaviour.GetResourcesToBeRemoved();
        

    }
}
