using HSProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int addItem = 0)
        {
            if (addItem > 0)
            {
                addToCart(addItem, Request.AnonymousID);
            }

            var success = createUser(Request.AnonymousID);

            var gameList = GetGameList();

            return View(gameList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MyCart()
        {
            ViewBag.Message = "My Cart";

            var cartList = GetCartList(Request.AnonymousID);
            

            return View(cartList);
        }

        public List<VideoGameModel> GetGameList()
        {
            var list = new List<VideoGameModel>();

            var adapter = new Models.VideoGamesTableAdapters.VideoGamesTableAdapter();
            var data = adapter.GetData();

            foreach (VideoGames.VideoGamesRow row in data)
            {
                var game = new VideoGameModel();
                game.Id = row.ID;
                game.GameName = row.GameName;
                game.Systems = row.Systems;
                game.Price = row.Price;
                game.Publisher = row.Publisher;
                game.CoverArt = row.CoverArt;

                list.Add(game);
            }

            return list;
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var adapter = new Models.VideoGamesTableAdapters.CartByUserTableAdapter();
            var data = adapter.GetCartByUser(Request.AnonymousID);

            ViewData["CartCount"] = data.Count();

            return PartialView("CartSummary");

        }

        public bool createUser(string userid)
        {
            var result = false;

            var adapter = new Models.VideoGamesTableAdapters.VideoGamesCartTableAdapter();
            var data = adapter.GetDataByUserID(userid);

            if(data.Count > 0)
            {
                result = true;
            }
            else
            {
                var status = adapter.Insert(userid, DateTime.Now, false);
                if (status > 0) result = true;
            }

            return result;

        }

        public void addToCart(int itemID, string userid)
        {
            var adapterCart = new Models.VideoGamesTableAdapters.VideoGamesCartTableAdapter();
            var adapterCartItems = new Models.VideoGamesTableAdapters.VideoGamesCartItemsTableAdapter();

            var cartData = adapterCart.GetDataByUserID(userid).FirstOrDefault();

            var itemInCart = adapterCartItems.GetDataByCartandItem(cartData.ID, itemID).FirstOrDefault();

            if (itemInCart != null)
            {
                itemInCart.SetField("Quantity", itemInCart.Quantity + 1);
            }
            else
            {
                adapterCartItems.Insert(cartData.ID, itemID, 1);
            }

        }

        public List<CartItemModel> GetCartList(string userid)
        {
            var list = new List<CartItemModel>();

            var adapter = new Models.VideoGamesTableAdapters.CartDisplayTableAdapter();
            var data = adapter.GetDataByUserID(userid);

            foreach (var row in data)
            {
                var cartItem = new CartItemModel();
                cartItem.UserID = row.UserID;
                cartItem.ProductID = row.ProductID;
                cartItem.CartID = row.CartID;
                cartItem.GameName = row.GameName;
                cartItem.CoverArt = row.CoverArt;
                cartItem.Price = row.Price;
                cartItem.Quantity = row.Quantity;
                cartItem.CartItemID = row.CartItemID;

                list.Add(cartItem);
            }

            return list;

        }

        public ActionResult Delete(int id)
        {
            var adapter = new Models.VideoGamesTableAdapters.VideoGamesCartItemsTableAdapter();
            var data = adapter.DeleteCartItem(id);

            return RedirectToAction("MyCart");
        }
    }
}