using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acceptInvitation : MonoBehaviour
{
    public Text invitationToYou;
    public Button accept, decline;
    void Start()
    {
        // se va schimba textul in functie de cine o sa-i dea invitatie de prietenie sau de joc
        // invitationToYou.text = "Do you accept the friendship invitation from ..."/ "Do you accept the game invitation from ..."
        // daca accepta accept  - va fi dus la friends si va vedea acel prieten trecut in lista/ se va duce la pagina de asteptare a jocului
        // daca accepta decline, doar va disparea pop-up-ul si i se va transmite celui care a dat invitatia un mesaj ca nu a acceptat sau ceva
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
