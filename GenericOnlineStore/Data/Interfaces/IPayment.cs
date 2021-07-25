using GenericOnlineStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.Data.Interfaces
{
    public interface IPayment
    {
        dynamic MakePayment(ATransaction transaction);
        dynamic MakePayment(APurchase transaction);

        ATransaction Deposit(ATransaction transaction);
        ATransaction Withdraw(ATransaction transaction);

        BaseAccount UpdateAccount(BaseAccount updatedaccount);
        BaseAccount CheckDetails(BaseAccount account);
        BaseAccount CheckDetails(string accountid);

        BaseAccount CreateAccount(BaseAccount newAccount);
        BaseAccount CreateAccount(string newAccount);
        List<BaseAccount> GetAllAccounts();
        List<ATransaction> GetAllTransactions();
    }
}
