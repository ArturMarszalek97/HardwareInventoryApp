using HardwareInventoryApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HardwareInventoryApp.Views
{
    /// <summary>
    /// Interaction logic for ColumnBarView.xaml
    /// </summary>
    public partial class ColumnBarView : Page
    {
        public ColumnBarView()
        {
            InitializeComponent();
            DataContext = new AnnualStatementViewModel();
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
                    if (currentMonth - 1 == 11)
                    {
                        this.AnnualStatements.Add(new AnnualStatement() { MonthToDisplay = months[i], Month = i + 1, TotalCostForMonth = 0 });
                    }
                    else
                    {
                        this.AnnualStatements.Add(new AnnualStatement() { MonthToDisplay = months[currentMonth + i], Month = currentMonth + 1, TotalCostForMonth = 0 });
                    }
                }

                var tras = Data.Items.Where(x => x.DateOfPurchase > DateTime.Now.AddYears(-1)).ToList();

                foreach (var statement in AnnualStatements)
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

        internal class AnnualStatement
        {
            public int Month { get; set; }

            public string MonthToDisplay { get; set; }

            public float TotalCostForMonth { get; set; }
        }
    }
}
