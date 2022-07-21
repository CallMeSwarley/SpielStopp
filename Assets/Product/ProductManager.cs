using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDBProductData;

/*IMPORTANT NOTE. The version of the Plugin we are using only supports the use of ENUMs and not String keys in the paid version. Please keep this in mind while working with the data. 
TLDR dont use the same GameID for two games and convert genre entries to the necesary enums if needed
*/
public class ProductManager : MonoBehaviour
{
    [SerializeField]
    Product product;
    // Start is called before the first frame update
    void Start()
    {
        //A name is given when instantiating to identify the gameDB when editing during play
        GameDB gameDb = new GameDB("ProductData");
        //Load the json from the resources folder
        gameDb.Load("GameDBs/gameDB");
        gameDb.OnDBLoaded = delegate () {
                //Access game data here once the DB is loaded
                var genre = gameDb.GameDataTable.GetByKey(GameDataSchema.KeyExampleGame1);
                Debug.Log(genre.Genre1Val);
                foreach (var rating in gameDb.GameDataTable.GetRows()) {
                    Debug.Log(rating.Value.Genre1Val);
                }
            };
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateProdut(Transform position, int id) {
       Product newObject = Instantiate(product, position);
       

    }
}
