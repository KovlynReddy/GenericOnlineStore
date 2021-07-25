using GenericOnlineStore.Data.Interfaces;
using GenericOnlineStore.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Data.Repository
{
    public class PaymentRepository : IPayment
    {
        private readonly ApplicationDbContext db;

        public PaymentRepository(ApplicationDbContext Db)
        {
            db = Db;
        }
        public BaseAccount CheckDetails(BaseAccount account)
        {
            return db.accounts.Find(account);
        }

        public BaseAccount CheckDetails(string accountid)
        {
            var accounts = db.accounts.ToList();
            return accounts.FirstOrDefault(m => m.UserId == accountid);
        }

        public BaseAccount CreateAccount(BaseAccount newAccount)
        {
            db.accounts.Add(newAccount);
            db.SaveChanges();

            return newAccount;
        }

        public BaseAccount CreateAccount(string newAccount)
        {
            var profile = CheckDetails(newAccount);
            if (profile == new BaseAccount() || profile == null)
            {
                BaseAccount baseAccount = new BaseAccount();
                baseAccount.UserId = newAccount;
                baseAccount.Balance = 0;
                baseAccount.AccountId = Guid.NewGuid().ToString();
                baseAccount.AccountHistory = Guid.NewGuid().ToString();
                CreateAccount(baseAccount);
            }
            return CheckDetails(newAccount);
        }

        public ATransaction Deposit(ATransaction transaction)
        {
            var account = CheckDetails(transaction.SenderId);
            account.Balance += transaction.Value;

            UpdateAccount(account);

            return transaction;


        }   

        public List<BaseAccount> GetAllAccounts()
        {
            return db.accounts.ToList();
        }

        public List<ATransaction> GetAllTransactions()
        {
            return db.transactions.ToList();
        }

        public dynamic MakePayment(ATransaction transaction)
        {
            var account = CheckDetails(transaction.SenderId);
            account.Balance += transaction.Value;

            // add transaction
            db.transactions.Add(transaction);
           

            UpdateAccount(account);
            db.SaveChanges();
            return transaction;
        }

        public dynamic MakePayment(APurchase transaction)
        {

            return transaction;

        }

        public BaseAccount UpdateAccount(BaseAccount updatedaccount)
        {
            var account = CheckDetails(updatedaccount.UserId);

            account.Balance = updatedaccount.Balance;

            db.Update(account);
            db.Entry(account).State = EntityState.Modified;

            db.SaveChanges();
            return account;
        }

        public ATransaction Withdraw(ATransaction transaction)
        {
            var account = CheckDetails(transaction.SenderId);
            account.Balance += transaction.Value;

            UpdateAccount(account);

            return transaction;

        }
    }
}
