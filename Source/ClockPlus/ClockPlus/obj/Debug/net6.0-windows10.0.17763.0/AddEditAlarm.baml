   M S B A M L   `   `   `  ����D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus  5   	       ClockPlus.AddEditAlarm   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClockPlus     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     � Title$  AddEditAlarm��5   	   $	��610��6   $	��509��6+    � WindowStartupLocation$ CenterScreen=�67      MaterialDesignBody#���   5
   	   ���	#FFFAFAFA$��Medium"�5   	   $��14(�5   	    � 
ResizeMode$ NoResize=�5   :   ��6	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ txtbox_title��5      )  #MaterialDesignThemes.Wpf.HintAssist   
HelperText$  ��5         HelperTextFontSize$ 16Q�5         Hint$ Alarm Title��5      $��
18,49,18,0q�60   $��26��6D   $	��Top=�6P   6h   )  #MaterialDesignThemes.Wpf.TimePicker  5   
   -   5          Name$ tp_alarmTime��5   $   	   SelectedTime	 12:00 PM6:   $��18,106,18,0q�6S   $	��Top=�6h   $��34��6�   4  .System.Windows.Automation.AutomationProperties
   HelpText$
 
alarm time��6�   6�   �� 5   
   -   5         ��Name$ cb_alarmType��5      -  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��5      $ Search��5         HintOpacity$	 .26Q�5       �� 
IsEditable$
 True��5       $��18,178,18,0q�6   $	��Top=�6*    �� SelectedIndex$ 0��6B   $��33��6T   .@�5!      �� $��default��5"      �� 5#      $��specificDate��5$      �� 5%      $��specificDates��5&      �� 5'      $��
everyMonth��5(      �� 5)      $��	everyYear��5*      5+       � 5,   
   -   5          �Name$ DateValueFrame��5,      $��Frame��6(   $��Center=�68   $	��154��6U   $��	0,245,0,0q�6b   $	��Top=�6u   $	��473��6�     � NavigationUIVisibility$ Hidden=�6�   �� 5-   
   $��
alarm time��6   $��209,112,0,0q�6%   ���	#FF3A3A3A���	#00000000$
��Left=�6h   $	��188��6�   $��22��6�   $	��Top=�6�   ${�False��6�    J� IsHitTestVisible$ False��6�   z�5.      V� 5/      .~�50      �� �� 51      �� 52      T� 53      54      56      �� 57   
   $��
alarm type��6   $��209,182,0,0q�6%   ���	#FF3A3A3A���	#00000000$
��Left=�6h   $	��174��6�   $��24��6�   $	��Top=�6�   ${�False��6�   $ False��6�   z�58      V� 59      .~�5:      �� �� 5;      �� 5<      T� 5=      5>      5@      �� 5A   
   -   5         ��Name$ btn_save��5A      $
��Save��6#   $��134,516,0,0q�62   ���	#FFEAEAEA$��40��6^   $	��Top=�6j   $��20(�6�   $
��Left=�6�   $	��104��6�   �� 5B   
   -   5        $ 
btn_cancel��5B      $��Cancel��6%   $��250,516,0,0q�66   ���	#FFEAEAEA$��40��6b   $	��Top=�6n   $��20(�6�   ���	#FF487171
����� �$
��Left=�6�   $	��106��6�   �� 5C   
   -   5        $ cb_alarmSound��5C      $ 2��5D      $ Search��5E      $	 .26Q�5F      $
 True��5G      $��18,413,18,0q�6   $	��Top=�64   $ 0��6L   $��32��6^   .@�5H      �� $��default��5I      �� 5J      $��specificDate��5K      �� 5L      $��specificDates��5M      �� 5N      $��
everyMonth��5O      �� 5P      $��	everyYear��5Q      5R      �� 5S   
   $��alarm sound��6   $��209,417,0,0q�6&   ���	#FF3A3A3A���	#00000000$
��Left=�6i   $	��174��6�   $��24��6�   $	��Top=�6�   ${�False��6�   $ False��6�   z�5T      V� 5U      .~�5V      �� �� 5W      �� 5X      T� 5Y      5Z      5\      �� 5]   
   -   5        $ btn_cmds��5]      $��Manage Commands��6#   $��134,471,0,0q�6=   ���	#FFEAEAEA$��20(�6i   $
��Left=�6w   $	��222��6�   $��40��6�   $	��Top=�6�   5^      5_      