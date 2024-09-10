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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for CmdFrameDefault.xaml
    /// </summary>
    public partial class CmdFrameDefault : Page
    {
        public Window window;

        public CmdFrameDefault()
        {
            InitializeComponent();
        }

        private void btn_openDialog_Click(object sender, RoutedEventArgs e)
        {
            // open file dialog
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();


            window.Activate();

            // set initial directory
            dialog.InitialDirectory = "C:\\Users";

            // set to allow folders
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                cmd_input.Text = dialog.FileName;
               
            }
            window.Activate();
        }

        private void btn_openDialogFile_Click(object sender, RoutedEventArgs e)
        {
            // open file dialog
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();


            window.Activate();

            // set initial directory
            dialog.InitialDirectory = "C:\\Users";

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                cmd_input.Text = dialog.FileName;

            }
            window.Activate();
        }
    }
}
