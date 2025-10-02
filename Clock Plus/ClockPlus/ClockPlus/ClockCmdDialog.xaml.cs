using ClockLib;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Drawing.Imaging;

namespace ClockPlus
{
    /// <summary>
    /// Interaction logic for ClockCmdDialog.xaml
    /// </summary>
    public partial class ClockCmdDialog : Window
    {
        public CmdFrameDefault cmdPage_default = new CmdFrameDefault();

        public ListBoxItem selected_lbItem = null;
        public int selected_lbIndex = -1;
        public ClockCommandType selectedCmd = ClockCommandType.Unknown;

        public List<ClockCommand> selectedCommands = new List<ClockCommand>();


        public bool saved = false;
        public ClockCmdDialog()
        {
            InitializeComponent();
            cmdPage_default.window = this;
            LoadCommandTypes();
            Loaded += ClockCmdDialog_Loaded;
        }




        public void LoadCommands(List<ClockCommand> _cmds)
        {
            if(_cmds == null) { return; }
            foreach(ClockCommand _clockCommand in _cmds)
            {
                // create a new listbox item
                ListBoxItem lbItem = new ListBoxItem();

                // set command
                lbItem.Content = _clockCommand.type.ToString();




                // store clock command inside 'tag' as data
                lbItem.Tag = new ClockCommand
                {
                    type = _clockCommand.type,
                    args = _clockCommand.args
                };

                lb_cmds.Items.Add(lbItem);
            }
        }

        // create event handlers
        private void ClockCmdDialog_Loaded(object sender, RoutedEventArgs e)
        {
            btn_deleteCmd.Click += Btn_deleteCmd_Click;
            btn_addCmd.Click += Btn_addCmd_Click;
            btn_updateCmd.Click += Btn_updateCmd_Click;
            btn_save.Click += Btn_save_Click;
            btn_cancel.Click += Btn_cancel_Click;
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            selectedCommands = GetCommands();
            saved = true;

            this.Close();
        }

        public List<ClockCommand> GetCommands()
        {
            List<ClockCommand> clockCommands = new List<ClockCommand>();
            // loop through each listbox item
            for (int i = 0; i < lb_cmds.Items.Count; i++)
            {
                // get listbox item
                ListBoxItem lbItem_cmd = lb_cmds.Items.GetItemAt(i) as ListBoxItem;

                // get clock command
                ClockCommand clockCmd = lbItem_cmd.Tag as ClockCommand;

                // add clock command
                clockCommands.Add(clockCmd);

            }
            return clockCommands;
        }

        private void Btn_updateCmd_Click(object sender, RoutedEventArgs e)
        {

            // if selected listbox item exists
            if (selected_lbItem != null)
            {
                // get command type combobox content
                ComboBoxItem cbItem = cb_cmdType.SelectedItem as ComboBoxItem;
                string content = cbItem.Content as string;

                // convert 'command type' string to enum 'CommandType'
                object obj_commandType = ClockCommandType.Unknown;
                if(Enum.TryParse(typeof(ClockCommandType), content, out obj_commandType))
                {
                    // get command type as an enum
                    ClockCommandType commandType = (ClockCommandType)obj_commandType;

                    // get the current input
                    string arg = cmdPage_default.cmd_input.Text;

                    selected_lbItem.Content = content;
                    selected_lbItem.Tag = new ClockCommand
                    {
                        type = commandType,
                        args = new List<string> { arg }
                    };

                }
            }

            else
            {
                MessageBox.Show("no cmd selected to update");
            }

        }
        

        // add command
        private void Btn_addCmd_Click(object sender, RoutedEventArgs e)
        {
            string arg = cmdPage_default.cmd_input.Text;


            string cmdType = selectedCmd.ToString();
            // create a new listbox item
            ListBoxItem lbItem = new ListBoxItem();

            // set command
            lbItem.Content = cmdType;




            // store clock command inside 'tag' as data
            lbItem.Tag = new ClockCommand
            {
                type = selectedCmd,
                args = new List<string> { arg }
            };

            lb_cmds.Items.Add(lbItem);
        }

        // delete command
        private void Btn_deleteCmd_Click(object sender, RoutedEventArgs e)
        {
            if (selected_lbItem != null)
            {
                lb_cmds.Items.Remove(selected_lbItem);
                selected_lbItem = null;
            }
        }


        // load command types
        public void LoadCommandTypes()
        {
            // clear combobox
            cb_cmdType.Items.Clear();

            // loop through each command type
            foreach(ClockCommandType cmdType in ClockLib.ClockLib.commandTypes)
            {
                // add command type
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = cmdType.ToString();

                cb_cmdType.Items.Add(cbItem);
            }

            // set selected index
            cb_cmdType.SelectedIndex = 0;
        }


        // updates cmd frame
        private void cb_cmdType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCmdPage();
        }

        // updates cmd frame
        void UpdateCmdPage()
        {
            // get selected combobox item
            ComboBoxItem cbItem = cb_cmdType.SelectedItem as ComboBoxItem;

            if (cbItem != null)
            {
                string content = cbItem.Content as string;

                object obj_cmdType = ClockCommandType.Unknown;
                if(Enum.TryParse(typeof(ClockCommandType), content, out obj_cmdType))
                {
                    ClockCommandType cmdType = (ClockCommandType)obj_cmdType;
                    selectedCmd = cmdType;

                    switch (selectedCmd)
                    {
                        case ClockCommandType.Close_Application:
                            // disable open dialog button
                            cmdPage_default.btn_openDialog.IsEnabled = false;
                            cmdPage_default.btn_openDialogFile.IsEnabled = false;
                            MaterialDesignThemes.Wpf.HintAssist.SetHint(cmdPage_default.cmd_input, "Application Name (eg: Notepad )");
                            cmdPage_default.cmd_input.Text = "";

                            // set content
                            CmdFrame.Content = cmdPage_default;

                            break;
                        case ClockCommandType.Open_URL:
                            // disable open dialog button
                            cmdPage_default.btn_openDialog.IsEnabled = false;
                            cmdPage_default.btn_openDialogFile.IsEnabled = false;
                            MaterialDesignThemes.Wpf.HintAssist.SetHint(cmdPage_default.cmd_input, "URL Path (eg: 'https://www.google.com' )");
                            cmdPage_default.cmd_input.Text = "";

                            // set content
                            CmdFrame.Content = cmdPage_default;

                            break;
                        case ClockCommandType.Turn_Off_ComputerSleepMode:
                            CmdFrame.Content = null;
                            break;
                        case ClockCommandType.Turn_On_ComputerSleepMode:
                            CmdFrame.Content = null;
                            break;
                        case ClockCommandType.Restart_Computer:
                            CmdFrame.Content = null;
                            break;
                        case ClockCommandType.Shutdown_Computer:
                            CmdFrame.Content = null;
                            break;
                        default:
                            // disable open dialog button
                            cmdPage_default.btn_openDialog.IsEnabled = true;
                            cmdPage_default.btn_openDialogFile.IsEnabled = true;
                            MaterialDesignThemes.Wpf.HintAssist.SetHint(cmdPage_default.cmd_input, "File/Directory Path (eg: 'C:\' )");
                            cmdPage_default.cmd_input.Text = "";

                            // set content
                            CmdFrame.Content = cmdPage_default;
                            break;
                    }
                }
            }
        }

        private void lb_cmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected_lbItem = lb_cmds.SelectedItem as ListBoxItem;
            ShowSelectedCmdInfo();
            UpdateDeleteButton();
        }

        void ShowSelectedCmdInfo()
        {
            // if selected listbox item exists
            if(selected_lbItem != null)
            {
                // loop through each combobox command type item
                for (int i = 0; i < cb_cmdType.Items.Count; i++)
                {
                    // get combobox item
                    ComboBoxItem cbItem = cb_cmdType.Items.GetItemAt(i) as ComboBoxItem;

                    // get combobox content & listbox content
                    string cbItem_content = cbItem.Content as string;
                    string lbItem_content = selected_lbItem.Content as string;

                    // if it matches
                    if(cbItem_content == lbItem_content)
                    {
                        // update combobox
                        cb_cmdType.SelectedItem = cbItem;

                        // load command input
                        ClockCommand clockCommand = selected_lbItem.Tag as ClockCommand;
                        cmdPage_default.cmd_input.Text = clockCommand.args[0];
                        break;
                    }

                }
            }
        }

        void UpdateDeleteButton()
        {
            if(selected_lbItem != null)
            {
                btn_deleteCmd.IsEnabled = true;
                btn_updateCmd.IsEnabled = true;
            }
            else
            {
                btn_deleteCmd.IsEnabled = false;
                btn_updateCmd.IsEnabled = false;
            }
        }
    }
}
