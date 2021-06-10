using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTax
{
    public static class Program
    {

        #region Constants

        public const string ENTER_ITEMS = "Enter the items you are purchasing (hit enter twice to view your total)";
        public const string WELCOME = "Welcome to MyStore!";

        #endregion

        #region Main

        public static void Main(string[] args)
        {
            Console.WriteLine(WELCOME + "\n");
            Console.WriteLine(ENTER_ITEMS + "\n");

            ProcessSalesTax();

            Console.WriteLine("\nHit enter to exit.");
            Console.Read();
        }

        #endregion

        #region Helper Methods

        private static void FindOrAddItemsInfo(ItemsInfo newItemInfo, List<ItemsInfo> currentItems)
        {
            ItemsInfo foundItemsInfo = currentItems.FirstOrDefault(ci => ci.Match(newItemInfo));
            if (foundItemsInfo != null)
            {
                foundItemsInfo.Amount++;
                foundItemsInfo.Cost += newItemInfo.Cost;
                foundItemsInfo.TaxTotal += newItemInfo.TaxTotal;
                return;
            }

            currentItems.Add(newItemInfo);
        }

        private static string GetItem(string input)
        {
            int endIndex = input.IndexOf(" at ");
            return input.Substring(2, endIndex - 1);
        }

        private static double GetPrice(string input)
        {
            int endIndex = input.IndexOf(" at ");
            return Convert.ToDouble(input.Substring(endIndex + 4));
        }

        private static void PrintReceipt(ItemsInfo itemsInfo)
        {
            if (itemsInfo.Amount > 1)
            {
                Console.WriteLine(itemsInfo.Item + ": " + itemsInfo.Cost + itemsInfo.GetEachCost());
            }
            else
            {
                Console.WriteLine(itemsInfo.Item + ": " + itemsInfo.Cost.ToString("0.00"));
            }
        }

        private static void PrintTotals(double tax, double cost)
        {
            Console.WriteLine("Sales Tax: " + tax.ToString("0.00"));
            Console.WriteLine("Total: " + cost.ToString("0.00"));
        }

        private static void ProcessSalesTax()
        {
            List<ItemsInfo> saleItems = new List<ItemsInfo>();
            string input;

            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                ItemsInfo itemInfo = new ItemsInfo(GetItem(input), GetPrice(input));
                itemInfo.SetTaxes();
                FindOrAddItemsInfo(itemInfo, saleItems);
            }

            saleItems.ForEach(si => PrintReceipt(si));
            PrintTotals(saleItems.Sum(si => si.TaxTotal), saleItems.Sum(si => si.Cost));
        }

        #endregion

    }
}
