using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bid_REST_Service
{
    public class Bid
    {
    // Variables
    #region
    private string id;
    private string item;
    private int bidAmount;
    private string name;
    #endregion


    // Properties
    #region
    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Item
    {
        get { return item; }
        set { item = value; }
    }
    public int BidAmount
    {
        get { return bidAmount; }
        set { bidAmount = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    #endregion

    // Constructors
    #region
    public Bid(string id, string item, int bidAmount, string name)
    {
        Id = id;
        Item = item;
        BidAmount = bidAmount;
        Name = name;
    }

    // Empty constructor for JSON related operations
    public Bid()
    {

    }
    #endregion
    }
}
