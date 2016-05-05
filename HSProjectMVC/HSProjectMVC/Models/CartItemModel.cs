using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSProjectMVC.Models
{
    public class CartItemModel
    {
        public string GameName { get; set; }

        public decimal Price { get; set; }
        public string CoverArt { get; set; }
        public int Quantity { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int CartID { get; set; }

        public int CartItemID { get; set; }

        public CartItemModel()
        {

        }
    }
}