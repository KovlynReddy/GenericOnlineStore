using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericOnlineStore.NewFolder
{
    public class MakePayment
    {
		//public dynamic PayAsync(string cardnumber , int month ,int year ,string cvc,int value )
		//{
		//	StripeConfiguration.SetApiKey("sk_test_51JC8JCG2MVavbgryUwdvrzyh9i3SbTY7c7GcHmo6IAgkNjbSKEoOHtj3PXC1AwjZlrOLVlttgLnp5aXxrNrlR6tx00WDoXvNnP");

		//	var optionstoken = new TokenCreateOptions
		//	{
		//		Card = new Cred{
		//			Number = cardnumber,
		//			expMonth = month,
		//			Expeay = year,
		//			Cvc = cvc

		//		}
		//	};

		//	var servicetoken = new TokenService();
		//	Token stripetoken = servicetoken.Create(optionstoken);

		//		var options = new ChargeCreateOptions { 
		//		Amount = value,
		//		Currency = "",
		//		Description = "",
		//		Source = stripetoken.Id
		//	};

		//	var service = new ChargeService();
		//	Charge charge = service.Create(options);

		//	if(charge.Paid)
		//	{
		//		return "Success";
		//	}
		//	else 
		//	{
		//		return "Failed";
		//	}


		//}

	}
}
