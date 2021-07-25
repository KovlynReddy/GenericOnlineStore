using FluentEmail.Core;
using FluentEmail.Smtp;
using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Data.Repository;
using GenericOnlineStore.Models.DataModels;
using GenericOnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GenericOnlineStore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayment paymentDB;

        public IStoreRepository StoreDb { get; }

        public PaymentController(IPayment paymentDB , IStoreRepository storeDb)
        {
            this.paymentDB = paymentDB;
            StoreDb = storeDb;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit() { return View(); }
        [HttpPost]
        public IActionResult Deposit(DepositViewModel newDeposit) {

            var newtransaction = new ATransaction();
            newtransaction.AccountId = User.Identity.Name;
            newtransaction.SenderId = User.Identity.Name;
            newtransaction.Value = newDeposit.Amount;
            newtransaction.TransactionId = Guid.NewGuid().ToString();
            newtransaction.ExtraDetails = "Test";
            
            paymentDB.Deposit(newtransaction);

            return View(); 
        }
        [HttpGet]
        public IActionResult Withdraw() { return View(); }        
        
        [HttpPost]
        public IActionResult Withdraw(DepositViewModel newDeposit)
        {
            var newtransaction = new ATransaction();
            newtransaction.AccountId = User.Identity.Name;
            newtransaction.SenderId = User.Identity.Name;
            newtransaction.Value = newDeposit.Amount * -1 ;
            newtransaction.TransactionId = Guid.NewGuid().ToString();
            newtransaction.ExtraDetails = "Test";

            paymentDB.Deposit(newtransaction);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Checkout() {
            var model = new TrolleyViewModel();

            var purchases = StoreDb.GetAllPurchases();
            var myPurchases = purchases.Where(m => m.UserId == User.Identity.Name).ToList();
            var todaysPurchases = myPurchases.Where(m => m.TimePurchased.Date == DateTime.Today).ToList();
            todaysPurchases = todaysPurchases.Where(m => m.Buffer != "Purchased").ToList();
            //get all purchases related to this email
            // get all purchases made today
            var allItems = StoreDb.GetAllItems();

            if (myPurchases.Count == 0)
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

            var newtransaction = new ATransaction();
            newtransaction.AccountId = "KovlynAccountId";
            newtransaction.SenderId = User.Identity.Name;
            newtransaction.Value = model.FinalTotal * -1 ;
            newtransaction.TransactionId = Guid.NewGuid().ToString();
            newtransaction.ExtraDetails = "TestTransaction";

            paymentDB.MakePayment(newtransaction);

            await SendMail(model.ItemsPurchased);

            foreach (var item in todaysPurchases)
            {
                item.Buffer = "Purchased";
                item.PurchaseDetails = newtransaction.TransactionId;
                StoreDb.UpdatePurchase(item);
            }




            return RedirectToAction("Index","Home"); 
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }

        }
        public async Task SendMail(List<StoreItemViewModel> model) {
            string itemsList = "";

            foreach (var item in model)
            {
                itemsList += item.item.ItemName +" " +item.Quantity.ToString() + "  " + item.SubTotal.ToString()+  "\n";
            }
            #region mail1

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential("Techno Solutions01", "T3$0em01");
            client.Port = 587;
            client.EnableSsl = true;
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("TechnoSolutions0001@gmail.com",
               "Techno" + (char)0xD8 + "Solutions01", System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress("kovlyn.reddy@gmail.com");
            // Specify the message content.

            MailMessage message = new MailMessage(from, to);

            message.Body = "Ypu have Purchased" + itemsList;
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Thank you For your Purchase ! " + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.


            //client.Send(message);

            #endregion

            #region Mail2

            var sender = new SmtpSender(() => new SmtpClient(host:"smtp.gmail.com") { 
            EnableSsl =true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Port=587,
            Credentials = new NetworkCredential("TechnoSolutions0001@gmail.com", "T3$0em01")
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From(emailAddress: "TechnoSolutions0001@gmail.com")
                .To(emailAddress: "kovlyn.reddy@gmail.com", name: "Kovlyn Reddy")
                .Subject(subject: "Thanks for the Purchase !")
                .Body(body: "Items Purchase =" + itemsList)
                .SendAsync();

            #endregion
        }
    }
}
