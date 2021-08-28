using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Data.Repository;
using GenericOnlineStore.Models.DataModels;
using GenericOnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Controllers
{
    public class StoreController : Controller
    {
        public IStoreRepository _Db { get; set; }
        public IWebHostEnvironment Env { get; }

        public StoreController(IStoreRepository Db,IWebHostEnvironment env)
        {
            _Db = Db;
            Env = env;
        }
        // GET: StoreController
        public ActionResult Index()
        {
            return RedirectToAction("CreateItem");
        }

        
        public ActionResult ViewItem(string id)
        {
            var selectedItem = _Db.GetAllItems().FirstOrDefault(m => m.ItemId == id);
            return View(selectedItem);
        }

        public ActionResult ViewAllItems() {

            var allitems = _Db.GetAllItems();
            List<StoreItemViewModel> viewmodels = new List<StoreItemViewModel>();

            foreach (var item in allitems)
            {
                viewmodels.Add(StoreItemViewModel.ToViewModel(item));
            }

            return View(viewmodels);
            }

        // GET: StoreController/Create
        [HttpGet]
        public ActionResult CreateItem()
        {
            return View();
        }

        public ActionResult AddToCart(StoreItemViewModel model) {

            //purchase add for this user 
            APurchase newPurchase = new APurchase();
            // purchase made today
            newPurchase.UserId = User.Identity.Name;
            newPurchase.ItemId = model.item.ItemId;
            newPurchase.Quantity = model.Quantity;
            newPurchase.TimePurchased = DateTime.Now;
            newPurchase.APurchaseId = Guid.NewGuid().ToString();

            _Db.AddPurchase(newPurchase);

            return RedirectToAction("ViewAllItems");
        }


        public ActionResult RemoveFromCart(StoreItemViewModel buff)
        {
            var model = new TrolleyViewModel();

            var purchases = _Db.GetAllPurchases();
            var myPurchases = purchases.Where(m => m.UserId == User.Identity.Name).ToList();
            var todaysPurchases = myPurchases.Where(m => m.TimePurchased.Date == DateTime.Today).ToList();
            //get all purchases related to this email
            // get all purchases made today
            var allItems = _Db.GetAllItems();

            List<StoreItemViewModel> models = new List<StoreItemViewModel>();
            int total = 0;

            foreach (var item in todaysPurchases)
            {
                var purchase = StoreItemViewModel.ToViewModel(allItems.FirstOrDefault(k => k.ItemId == item.ItemId));
                purchase.SubTotal = purchase.item.Value;// * item.Quantity;
                total += purchase.SubTotal;
                models.Add(purchase);
            }

            var thepurchase = todaysPurchases.FirstOrDefault(m => m.ItemId == buff.item.ItemId);
            _Db.DeletePurchase(thepurchase);

            model.ItemsPurchased = models;
            model.FinalTotal = total;

            return RedirectToAction("ViewAllItems");
        }


        public ActionResult ViewMyTrolley() {

            var model = new TrolleyViewModel();

            var purchases = _Db.GetAllPurchases();
            var myPurchases = purchases.Where( m => m.UserId == User.Identity.Name).ToList();
            var todaysPurchases = myPurchases.Where( m => m.TimePurchased.Date == DateTime.Today).ToList();
            todaysPurchases = todaysPurchases.Where(m => m.Buffer != "Purchased").ToList();
            //get all purchases related to this email
            // get all purchases made today
            var allItems = _Db.GetAllItems();

            if (myPurchases.Count == 0 )
            {

            todaysPurchases.AddRange(myPurchases);

            }
            

            List<StoreItemViewModel> models = new List<StoreItemViewModel>();
            int total = 0;

            foreach (var item in todaysPurchases)
            {
                var purchase = StoreItemViewModel.ToViewModel(allItems.FirstOrDefault(k => k.ItemId == item.ItemId));
                purchase.SubTotal = purchase.item.Value * item.Quantity;
                total += purchase.SubTotal;
                purchase.Quantity = item.Quantity;
                models.Add(purchase);
            }


            
            model.ItemsPurchased = models;
            model.FinalTotal = total;
            return View(model);
        }


        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateItem(CreateItemViewModel newItem , IFormFile file)
        {
            newItem.ItemId = Guid.NewGuid().ToString();

            using (var ms = new MemoryStream())
            {

                string uploadsFolder = Path.Combine(Env.ContentRootPath,"wwwroot");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + newItem.uploadedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                newItem.ImagePath = uniqueFileName;
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    newItem.uploadedImage.CopyTo(stream);
                }

            }


            _Db.AddItem(newItem);

            return RedirectToAction("ViewAllItems","Store");
        }

        [HttpGet]
        public IActionResult ViewReciept(string id)
        {

            var model = new TrolleyViewModel();

            var purchases = _Db.GetAllPurchases();
            var myPurchases = purchases.Where(m => m.UserId == User.Identity.Name).ToList();
            var recieptpurchases = myPurchases.Where(m => m.PurchaseDetails == id).ToList();
            //get all purchases related to this email
            // get all purchases made today
            var allItems = _Db.GetAllItems();


            List<StoreItemViewModel> models = new List<StoreItemViewModel>();
            int total = 0;

            foreach (var item in recieptpurchases)
            {
                var purchase = StoreItemViewModel.ToViewModel(allItems.FirstOrDefault(k => k.ItemId == item.ItemId));
                purchase.SubTotal = purchase.item.Value * item.Quantity;
                total += purchase.SubTotal;
                purchase.Quantity = item.Quantity;
                models.Add(purchase);
            }


            model.DatePurchased = recieptpurchases.First().TimePurchased;
            model.ItemsPurchased = models;
            model.FinalTotal = total;
            // get reciept

            // get all purchases on reciept 

            // get item for each purchase 

            // get subtotal for purchase quantity * item cost 

            // get total of all items subtotals together 
            // get date time purchased 

            return View(model);

        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CheckOut() {

            return View();
        }
    }
}
