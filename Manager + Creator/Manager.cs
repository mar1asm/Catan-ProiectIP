

public sealed class Manager//Patronu'
{

    private RulesExpansion rules = new RulesExpansion();
    private BoardExpansion board= new BoardExpansion();
    private List<SettlementExpansion> settlements= new List<SettlementExpansion>();
    private List<ConnectorCreator> connectors = new List<ConnectorCreator>();
    private List<CardExpansion> cards= new List<CardExpansion>();
    private List<DeckExpansion> decks= new List<DeckExpansion>();
    private List<TileExpansion> tiles= new List<TileExpansion>();


    Manager(){
    }

    private static readonly object padlock= new object();
    private static Manager instance = null;
    public static Manager  Instance {
        get{
            if(instance == null){
                lock(padlock){
                    instance = new Manager();
                }
            }
            return instance;
        }
    }

    
    public RulesExpansion Rules{
        get { return rules;}
        set { rules = value;}
    }

    public BoardExpansion Board{
        get { return board;}
        set { board = value;}
    }    
    
    public  List<SettlementExpansion> Settlements{
        get { return settlements;}
        set { settlements = value;}
    }
    
    List<ConnectorCreator> Connectors{
        get { return connectors; }
        set { connectors = value; }
    }
         
    public List<TileExpansion> Tiles{
        get{ return tiles;}
        set{ tiles = value;}
    }
    public List<DeckExpansion> Decks{
        get{ return decks;}
        set{ decks = value;}
    }

    public List<CardExtension> Cards{
        get{ return cards;}
        set{ cards = value;}
    }


    public string toStringAvailableTiles(){
        string output = "\"availableTiles\": [";

        // {
        //     "type": "sheep",
        //     "number": 4
        // }
     foreach(Tiles currentTile in tileList)
     {
        if(output.IndexOf(output, 0, output.Length()) == '}'){
            output += ",\n";
        }
        else
            output += "\n";
        output += "{\n";

        output += "\"type\": " + "\"" + currentTile.type +"\",";

        output += "\"number\": " + nrOfTiles[currentTile] + "\n";

        output += "}";


     }

     output += "]";

     return output;
    }

    public void toFile(){
        
        string output = "";
        output = "{\n";
        output += toStringAvailableTiles() + ",\n";
        output += board.toString();
        output += "\n}";
        File.WriteAllText("board.txt", output);


    }
}
