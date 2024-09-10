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

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for AAPageSpecificDates.xaml
    /// </summary>
    public partial class AAPageSpecificDates : Page
    {
        public AAPageSpecificDates()
        {
            InitializeComponent();
        }

        private void pickDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = pickDate.SelectedDate;
            if (selectedDate != null)
            {
                ListBoxItem lbItem = new ListBoxItem();
                lbItem.Content = selectedDate.Value.Month + "/" + selectedDate.Value.Day + "/" + selectedDate.Value.Year;

                lb_dates.Items.Add(lbItem);
                // conitnue from here
            }
                
        }
    }
}
