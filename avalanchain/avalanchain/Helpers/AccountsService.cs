using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    public class AccountsService
    {
        public static bool SendMoney(Send send)
        {
            var isOk = true;
            var fromAcc = GetAccount(send.FromAccountNumber);
            var toAccount = new Account();

            if (string.IsNullOrEmpty(send.ReceiverId))
            {
                toAccount.AccountNumber = send.ReceiverAccountNumber;
                toAccount.Currency = send.CurrencyReceive;
                toAccount.CurrencyIcon = GetCurrencyIcon(send.CurrencyReceive);

            }
            else
            {
                toAccount = GetAccount(send.ReceiverAccountNumber);
                toAccount.Amount = toAccount.Amount + send.ReceiveAmount;
                isOk = UpdateAccount(toAccount);
            }

            if (fromAcc.Amount < send.FromAmount || isOk==false)
            {
                 return false;
            }
            fromAcc.Amount = fromAcc.Amount - send.FromAmount;
            isOk = UpdateAccount(fromAcc);

            if (isOk == false)
            {
                return false;
            }

            var icon = FontAwesome.FAAngleDoubleRight;
            TransactionType transac = TransactionType.Sent;

            var percent = send.FromAmount / 100;
            var fee = (decimal)percent;

            var transaction= new Transaction
            {
                Id = Guid.NewGuid(),
                From = fromAcc.AccountNumber,
                To = toAccount.AccountNumber,
                Description = transac + " via Avalanchain payment system",
                Currency = fromAcc.Currency,
                CurrencyIcon = fromAcc.CurrencyIcon,
                CurrencyReceive = toAccount.Currency,
                CurrencyReceiveIcon = toAccount.CurrencyIcon,
                TypeIcon = icon,
                Type = transac,
                Amount = (decimal)send.FromAmount,
                AmountReceive = (decimal)send.ReceiveAmount,
                DateTime = DateTime.UtcNow,
                Fee = fee
            };

            AddNewTransaction(transaction);
            return isOk;
        }

        public void Pay(string from, string to, decimal amountSend)
        {
            var icon = FontAwesome.FAAngleDoubleRight;
            TransactionType transac;
            transac = TransactionType.Pay; icon = FontAwesome.FAShoppingCart;

        }

        public static void AddNewTransaction(Transaction transaction)
        {
            SampleData.Transactions.Add(transaction);
        }

        public static Transaction TransformReceiverTransaction(Transaction transaction)
        {
            if (transaction.Type == TransactionType.Sent)
            {
                var from = transaction.From;
                var to = transaction.To;
                transaction.From = to;
                transaction.To = from;
                transaction.TypeIcon = FontAwesome.FAAngleDoubleLeft;
                transaction.Type = TransactionType.Receive;
                transaction.Description = TransactionType.Receive + " via Avalanchain payment system";

            }
            return transaction;
        }

        //public enum CurrencyType
        //{
        //    BTC, USD, GBP, EUR
        //}
        //public List<CurrencyIcon> CurrencyIcon
        //{
        //     EUR = Convert.ToInt32(FontAwesome.FAEur),
        //}
        public static string GetCurrencyIcon(CurrencyType icon)
        {
            var currencyIcon = "";
            switch (icon)
            {
                case CurrencyType.EUR:
                    currencyIcon = FontAwesome.FAEur;
                    break;
                case CurrencyType.BTC:
                    currencyIcon = FontAwesome.FABtc;
                    break;
                case CurrencyType.GBP:
                    currencyIcon = FontAwesome.FAGbp;
                    break;
                case CurrencyType.USD:
                    currencyIcon = FontAwesome.FAUsd;
                    break;
            }
            return currencyIcon;
        }

        public static string GetCurrencyPrice(CurrencyType type, CurrencyPricing currencyPricing)
        {
            var currencyPrice = "";
            switch (type)
            {
                case CurrencyType.EUR:
                    currencyPrice = currencyPricing.EUR;
                    break;
                case CurrencyType.BTC:
                    currencyPrice = "1";
                    break;
                case CurrencyType.GBP:
                    currencyPrice = currencyPricing.GBP;
                    break;
                case CurrencyType.USD:
                    currencyPrice = currencyPricing.USD;
                    break;
            }
            return currencyPrice;
        }

        public static Account GetAccount(Guid accountId)
        {
            return SampleData.Accounts.First(a => a.Id == accountId);
        }

        public static bool UpdateAccount(Account account)
        {
            var isOk = true;
            try
            {
                SampleData.Accounts.First(a => a.AccountNumber == account.AccountNumber).Amount = account.Amount;
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public static Account GetAccount(string accountNumber)
        {
            return SampleData.Accounts.First(a => a.AccountNumber == accountNumber);
        }

        public void AddAccount(string accountId)
        {

        }

        public void DeleteAccount(string accountId)
        {

        }
    }


}
