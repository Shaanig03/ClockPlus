   M S B A M L   `   `   `  ���� D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus      ClockPlus.AddEditTimer   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:ClockPlus     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  AddEditTimer��$	��389��$	��509�����	#FFFAFAFA � WindowStartupLocation$ CenterScreen=�.��� .+��� -    ��Name$ txtbox_title��)  #MaterialDesignThemes.Wpf.HintAssist   
HelperText$  ��   HelperTextFontSize$ 16Q�   Hint$ Timer Title��$��
18,49,18,0q�$��26��$	��Top=�)  #MaterialDesignThemes.Wpf.TimePicker  -     Name$ tp_time��$
 Time��   	Is24Hours$
 True��   WithSeconds$
 True��$��18,106,18,0q�$	��Top=�$��34��	   SelectedTime	 03/04/2024 00:05:00�� -   
 ��Name$
 btn_save��$
��Save��$��144,290,0,0q����	#FFEAEAEA$��40��$	��Top=�$��20(�$
��Left=�$	��104���� -   $
 
btn_cancel��$��Cancel��$��260,290,0,0q����	#FFEAEAEA$��40��$	��Top=�$��20(����	#FF487171
����� �$
��Left=�$	��106���� -    ��Name$ cb_timerSound��-  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��$ Search��   HintOpacity$	 .26Q� �� 
IsEditable$
 True��$��18,180,18,0q�$	��Top=� �� SelectedIndex$ 0��$��32���� $��alarm sound��$��209,184,0,0q����	#FF3A3A3A���	#00000000$
��Left=�$	��174��$��24��$	��Top=�${�False�� J� IsHitTestVisible$ False��z�V� .~��� �� �� T� �� -   $
 btn_cmds��$��Manage Commands��$��144,245,0,0q����	#FFEAEAEA$��20(�$
��Left=�$	��222��$��40��$	��Top=�