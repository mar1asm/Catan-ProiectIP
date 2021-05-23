using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerHolderBehaviour : MonoBehaviour
{
    
    
    [System.Serializable]
    public class ColorBannerPair
    {
        public PlayerColor color;
        public GameObject banner;

        public ColorBannerPair(PlayerColor color, GameObject banner)
        {
            this.color = color;
            this.banner = banner;
        }
    }

    [SerializeField]
    private ColorBannerPair[] banners;



    void Start()
    {

    }


    public void UpdateScore(string username, int score) {
        foreach (Transform child in transform)
        {
            BannerBehaviour banner = child.gameObject.GetComponent<BannerBehaviour>();
            if(banner.GetText() == username) {
                banner.SetScore(score.ToString());
                break;
            }
        }
    }
    public void SetHighlight(string username) {
        foreach (Transform child in transform)
        {
            BannerBehaviour banner = child.gameObject.GetComponent<BannerBehaviour>();
            banner.SetHighlight(banner.GetText() == username);
        }
    }


    public void AddBanner(Player p)
    {
        AddBanner(p.nickname, p.color);
    }

    public void AddBanner(string name, PlayerColor color)
    {
        foreach (var colorBannerPair in banners)
        {
            if(colorBannerPair.color == color)
            {
                
                var gameObject =  Instantiate(colorBannerPair.banner, transform);
                gameObject.GetComponent<BannerBehaviour>().SetText(name);
                gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                return;
            }
        }
    }
}
