using HardwareInventoryApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HardwareInventoryApp.Views
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : Window
    {
        public StatisticView()
        {
            InitializeComponent();

            DataContext = new ItemPieViewModel();
            this.Frame.Content = new TestPageView();
            this.SecondFrame.Content = new ColumnBarView();
        }

        internal class ItemPieViewModel
        {
            public List<ItemPie> List { get; private set; }

            public ItemPieViewModel()
            {
                List = new List<ItemPie>();
                this.PrepareList();
            }

            private void PrepareList()
            {
                List<string> categories = new List<string>();

                foreach (var item in Data.Items)
                {
                    if (!categories.Contains(item.Category))
                    {
                        categories.Add(item.Category);
                        this.List.Add(new ItemPie() { Category = item.Category, TotalCost = 0 });
                    }
                }

                foreach (var category in categories)
                {
                    foreach (var item in Data.Items)
                    {
                        if (item.Category == category)
                        {
                            this.List.Where(x => x.Category == category).Single().TotalCost += item.Price;
                        }
                    }
                }

                // prepare second part

                List<string> months = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
                var currentMonth = DateTime.Now.Month;

                for (int i = 0; i < 12; i++)
                {
                    if (currentMonth - 1 == 11)
                    {
                        this.List.Add(new ItemPie() { MonthToDisplay = months[i], Month = i + 1, TotalCostForMonth = 0 });
                    }
                    else
                    {
                        this.List.Add(new ItemPie() { MonthToDisplay = months[currentMonth + i], Month = currentMonth + 1, TotalCostForMonth = 0 });
                    }
                }

                var tras = Data.Items.Where(x => x.DateOfPurchase > DateTime.Now.AddYears(-1)).ToList();

                foreach (var statement in List)
                {
                    if (string.IsNullOrEmpty(statement.MonthToDisplay))
                        continue;

                    foreach (var item in tras)
                    {
                        if (statement.Month == item.DateOfPurchase.Month)
                        {
                            statement.TotalCostForMonth += item.Price;
                        }
                    }
                }
            }
        }

        internal class ItemPie
        {
            public string Category { get; set; }
            public float TotalCost { get; set; }

            public int Month { get; set; }

            public string MonthToDisplay { get; set; }

            public float TotalCostForMonth { get; set; }

            public ItemPie()
            {

            }
        }

        internal class AnnualStatementViewModel
        {
            public List<AnnualStatement> AnnualStatements { get; private set; }

            public AnnualStatementViewModel()
            {
                AnnualStatements = new List<AnnualStatement>();
                this.PrepareList();
            }

            private void PrepareList()
            {
                List<string> months = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
                var currentMonth = DateTime.Now.Month;

                for (int i = 0; i < 12; i++)
                {
                    this.AnnualStatements.Add(new AnnualStatement() { Month = months[currentMonth - 1], TotalCost = 0 });
                }

                var tras = Data.Items.Where(x => x.DateOfPurchase > DateTime.Now.AddYears(-1)).ToList();

                foreach (var statement in AnnualStatements)
                {
                    foreach (var item in tras)
                    {
                        if (statement.Month == item.DateOfPurchase.Month.ToString())
                        {
                            statement.TotalCost += item.Price;
                        }
                    }
                }
            }
        }

        internal class AnnualStatement
        {
            public string Month { get; set; }

            public float TotalCost { get; set; }
        }
    }
}
