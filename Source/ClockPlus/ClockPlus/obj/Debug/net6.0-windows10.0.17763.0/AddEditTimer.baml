   M S B A M L   `   `   `  ����D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus  5   	       ClockPlus.AddEditTimer   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClockPlus     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  AddEditTimer��5	   	   $	��389��6   $	��509��6+   ���	#FFFAFAFA � WindowStartupLocation$ CenterScreen=�6N    � 
ResizeMode$ NoResize=�6s   .��5
      � .+�5   
   �� -   5         ��Name$ txtbox_title��5   	   )  #MaterialDesignThemes.Wpf.HintAssist   
HelperText$  ��5   	      HelperTextFontSize$ 16Q�5   	      Hint$ Timer Title��5   	   $��
18,49,18,0q�66   $��26��6J   $	��Top=�6V   )  #MaterialDesignThemes.Wpf.TimePicker  5   
   -   5          Name$ tp_time��5      $
 Time��5         	Is24Hours$
 True��5      	   WithSeconds$
	 True��5      $��18,106,18,0q�6   $	��Top=�6+   $��34��6C   
   SelectedTime
 03/04/2024 00:05:006O   �� 5   
   -   5         ��Name$ btn_save��5      $
��Save��6#   $��144,290,0,0q�62   ���	#FFEAEAEA$��40��6^   $	��Top=�6j   $��20(�6�   $
��Left=�6�   $	��104��6�   6�   �� 5   
   -   5        $ 
btn_cancel��5      $��Cancel��6%   $��260,290,0,0q�66   ���	#FFEAEAEA$��40��6b   $	��Top=�6n   $��20(�6�   ���	#FF487171
����� �$
��Left=�6�   $	��106��6�   �� 5   
   -   5         ��Name$ cb_timerSound��5      -  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��5   	   $ Search��5   	      HintOpacity$	 .26Q�5   	    �� 
IsEditable$
 True��5   	   $��18,180,18,0q�6   $	��Top=�60    �� SelectedIndex$ 0��6H   $��32��6Z   5      �� 5   
   $��alarm sound��6   $��209,184,0,0q�6&   ���	#FF3A3A3A���	#00000000$
��Left=�6i   $	��174��6�   $��24��6�   $	��Top=�6�   ${�False��6�    J� IsHitTestVisible$ False��6�   z�5      V� 5      .~�5       �� �� 5!      �� 5"      T� 5#      5$      5&      �� 5'   
   -   5        $ btn_cmds��5'      $��Manage Commands��6#   $��144,245,0,0q�6=   ���	#FFEAEAEA$��20(�6i   $
��Left=�6w   $	��222��6�   $��40��6�   $	��Top=�6�   6�   5)      5*      