   M S B A M L   `   `   `  ���� D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus      ClockPlus.ClockCmdDialog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:ClockPlus     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Manage Commands��$	��610��$	��509�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ 
cb_cmdType��-  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search��   HintOpacity$	 .26Q� �� 
IsEditable$
 True��$��
18,41,18,0q�$	��Top=� �� SelectedIndex$ 0��$��33��.@��� $��default���� $��specificDate���� $��specificDates���� $��
everyMonth���� $��	everyYear���� $��command type��$��
209,45,0,0q����	#FF3A3A3A���	#00000000$
��Left=�$	��174��$��24��$	��Top=�${�False��	 J� IsHitTestVisible$	 False��z�V� .~��� �� �� T� �� -   
 ��Name$
 btn_save��$
��Save��$��134,516,0,0q����	#FFEAEAEA$��40��$	��Top=�$��20(�$
��Left=�$	��104���� -   $
 
btn_cancel��$��Cancel��$��250,516,0,0q����	#FFEAEAEA$��40��$	��Top=�$��20(����	#FF487171
����� �$
��Left=�$	��106�� � -     �Name$ CmdFrame��$	��0.7q�$��Frame��$��Center=�$	��116��$��0,96,0,0q�$	��Top=�$	��470��
�������  � NavigationUIVisibility$ Hidden=��� -    ��Name$ lb_cmds��$��Center=�$	��207��$��	0,286,0,0q�$	��Top=�$	��473���� -   $
 
btn_addCmd��$��Add Cmd+��$��
73,217,0,0q����	#FFEAEAEA$��29��$	��Top=�$��12(�$
��Left=�$��93���� -   $
 btn_deleteCmd��${�false��$��Delete (Selected) Cmd��$��283,217,0,0q����	#FFEAEAEA$��29��$	��Top=�$��12(����	#FFB9543D���	#FFB9543D$
��Left=�$	��162���� -   $
 btn_updateCmd��$��
Update Cmd��$��171,217,0,0q����	#FFEAEAEA$��29��$	��Top=�$��12(�$
��Left=�$	��107���� $+��%// continue command management dialog��$
��Left=�$��30��$��134,251,0,0q�$	��Top=�$	��249��