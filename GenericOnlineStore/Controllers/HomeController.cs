using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Data.Repository    ;
using GenericOnlineStore.Models;
using GenericOnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPayment paymentrepo;
        private readonly IStoreRepository storedb;

        public HomeController(ILogger<HomeController> logger,IPayment paymentrepo, IStoreRepository storedb)
        {
            _logger = logger;
            this.paymentrepo = paymentrepo;
            this.storedb = storedb;
        }

        public IActionResult Index()
        {
            var profile = paymentrepo.CreateAccount(User.Identity.Name);

            return View();
        }

        [HttpGet]
        public IActionResult ViewMyAccount(){
            var profile = paymentrepo.CheckDetails(User.Identity.Name);

            var model = new AccountViewModel();
            model.AccountId = profile.AccountId;
            model.Total = profile.Balance;
            model.UserId = User.Identity.Name;

            var purchases = paymentrepo.GetAllTransactions();
            var mypurchases = purchases.Where(m => m.SenderId == User.Identity.Name).ToList();
                var allpurchases =storedb.GetAllPurchases();

            foreach (var purchase in mypurchases)
            {
               var purchasedetails =  allpurchases.Where( m=>m.PurchaseDetails == purchase.TransactionId).ToList();
            }

            model.purchaseHistory = mypurchases; 

            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Links()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewReciept(string id) {

            var model = new TrolleyViewModel();

            var purchases = storedb.GetAllPurchases();
            var myPurchases = purchases.Where(m => m.UserId == User.Identity.Name).ToList();
            var recieptpurchases = myPurchases.Where(m => m.PurchaseDetails == id).ToList();
            //get all purchases related to this email
            // get all purchases made today
            var allItems = storedb.GetAllItems();


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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
