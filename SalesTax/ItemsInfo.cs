using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTax
{
    public class ItemsInfo
    {

        #region Constants

        public const string BOOK = "book";
        public const string CHOCOLATE = "chocolate";
        public const string CHOCOLATES = "chocolates";
        public const string IMPORT = "imported";
        public const string Music = "music";
        public const string PILLS = "pills";

        private const double IMPORT_TAX = .05;
        private const double SALES_TAX = .10;

        #endregion

        #region Constructor

        public ItemsInfo(string item, double cost)
        {
            Amount = 1;
            Cost = cost;
            Item = item;
        }

        #endregion

        #region Properties

        public int Amount { get; set; }

        public double Cost { get; set; }

        public string Item { get; set; }

        public double TaxTotal { get; set; }

        #endregion

        #region Public Methods

        public string GetEachCost()
        {
            double eachCost = Cost / Amount;
            return " (" + Amount + " @ " + eachCost.ToString("0.00") + ")";
        }

        public bool Match(ItemsInfo sourceItemsInfo)
        {
            string lowerItem = Item.ToLower();
            return lowerItem.Equals(sourceItemsInfo.Item.ToLower());
        }

        public void SetTaxes()
        {
            if (IsUnTaxed() && !IsImported())
            {
                return;
            }

            CalculateImportTax();
            CalculateSalesTax();
            AddTax();
        }

        #endregion

        #region Helper Methods

        private void AddTax()
        {
            Cost += TaxTotal;
        }

        private void CalculateImportTax()
        {
            if (!IsImported())
            {
                return;
            }

            TaxTotal += Math.Ceiling(Cost * IMPORT_TAX * 20) / 20;
        }

        private void CalculateSalesTax()
        {
            if (IsUnTaxed())
            {
                return;
            }

            TaxTotal += Math.Ceiling(Cost * SALES_TAX * 20) / 20;
        }

        private bool IsImported()
        {
            string lowerItem = Item.ToLower();
            return lowerItem.Contains(IMPORT);
        }

        private bool IsUnTaxed()
        {
            string lowerItem = Item.ToLower();
            return
                   lowerItem.Contains(BOOK) ||
                   lowerItem.Contains(CHOCOLATE) ||
                   lowerItem.Contains(CHOCOLATES) ||
                   lowerItem.Contains(PILLS);
        }

        #endregion

    }
}
