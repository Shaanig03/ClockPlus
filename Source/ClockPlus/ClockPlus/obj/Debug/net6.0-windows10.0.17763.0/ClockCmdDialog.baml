   M S B A M L   `   `   `  ����D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus  5   	       ClockPlus.ClockCmdDialog   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClockPlus     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Manage Commands��5	   	   $	��610��6!   $	��509��6.    � 
ResizeMode$ NoResize=�6:    � WindowStartupLocation$ CenterScreen=�6P   .��5
      � .+�5   
   �� -   5         ��Name$ 
cb_cmdType��5      -  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$ 2��5   	   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search��5   	      HintOpacity$	 .26Q�5   	    �� 
IsEditable$
 True��5   	   $��
18,41,18,0q�6   $	��Top=�6/    �� SelectedIndex$ 0��6G   $��33��6Y   .@�5      �� $��default��5      �� 5      $��specificDate��5      �� 5      $��specificDates��5      �� 5      $��
everyMonth��5      �� 5      $��	everyYear��5      5      �� 5   
   $��command type��6   $��
209,45,0,0q�6'   ���	#FF3A3A3A���	#00000000$
��Left=�6i   $	��174��6�   $��24��6�   $	��Top=�6�   ${�False��6�   	 J� IsHitTestVisible$	 False��6�   z�5      V� 5      .~�5       �� �� 5!      �� 5"      T� 5#      5$      5&      �� 5'   
   -   5        
 ��Name$
 btn_save��5'      $
��Save��6#   $��134,516,0,0q�62   ���	#FFEAEAEA$��40��6^   $	��Top=�6j   $��20(�6�   $
��Left=�6�   $	��104��6�   �� 5(   
   -   5        $
 
btn_cancel��5(      $��Cancel��6%   $��250,516,0,0q�66   ���	#FFEAEAEA$��40��6b   $	��Top=�6n   $��20(�6�   ���	#FF487171
����� �$
��Left=�6�   $	��106��6�    � 5)   
   -   5          �Name$ CmdFrame��5)      $	��0.7q�6"   $��Frame��68   $��Center=�6H   $	��116��6e   $��0,96,0,0q�6r   $	��Top=�6�   $	��470��6�   
�������  � NavigationUIVisibility$ Hidden=�6�   �� 5*   
   -   5         ��Name$ lb_cmds��5*      $��Center=�6#   $	��207��6@   $��	0,286,0,0q�6M   $	��Top=�6`   $	��473��6x   6�   �� 5+   
   -   5        $
 
btn_addCmd��5+      $��Add Cmd+��6%   $��
73,217,0,0q�68   ���	#FFEAEAEA$��29��6c   $	��Top=�6o   $��12(�6�   $
��Left=�6�   $��93��6�   �� 5,   
   -   5        $
 btn_deleteCmd��5,      ${�false��6(   $��Delete (Selected) Cmd��6:   $��283,217,0,0q�6Z   ���	#FFEAEAEA$��29��6�   $	��Top=�6�   $��12(�6�   ���	#FFB9543D���	#FFB9543D$
��Left=�6�   $	��162��6  �� 5-   
   -   5        $
 btn_updateCmd��5-      $��
Update Cmd��6(   $��171,217,0,0q�6=   ���	#FFEAEAEA$��29��6i   $	��Top=�6u   $��12(�6�   $
��Left=�6�   $	��107��6�   5/      50      