   M S B A M L   `   `   `  ����D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus  5   	       ClockPlus.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClockPlus     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � Title$  
Clock Plus��5   	   $	��676��6   $
��1234��6)      MaterialDesignBody#���   5
   	     MaterialDesignPaper#���  5   	   $��Medium"�5   	   $��14(�5   	    � Icon$ 
/icon2.png��5   S    � 
ResizeMode$ NoResize=�6e    � WindowStartupLocation$ CenterScreen=�6{   ��6	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � .+�5   
   #  MaterialDesignThemes.Wpf.Card  .��5      �� -   5         ��Name$ 
tabControl��5      3  -MaterialDesignThemes.Wpf.NavigationRailAssist   ShowSelectionBackground$
 True��5       J� SnapsToDevicePixels$
 True��5       * &MaterialDesignNavigationRailTabControl#��[ 5       �� TabStripPlacement$
 Left=�5      .@�5      �� -   5         ��Name$ TabAlarm��5      ��5      �� 5      $
��auto��5      $
��auto��5      .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  $��24��5      $��24��5      $��Center=�5       	   Kind$	 ProgressClock=�5!      �� 5"      $��Center=�5#      $��Alarm��5$      5%      .��5'      � 
 � RowDefinitions
 5(      �� 5)      $	��76* �6,   �� 5*      $
��585* �6,   .+�5-      �� $��
10,24,0,74q�6'   $��Visible=�6;   $��Hidden=�6a   $��2��6�   .��5.      � -   5         �Name$ alarm_container��5.   (   $
��Left=�6A   $
��1136��6\   5/      �� 50      -   5         ��Name$ btn_addAlarm��50   !   $��+��67   $��	0,0,16,20q�6C   ���	#FFEAEAEA$��40��6m   $��Bottom=�6y   $��Right=�6�   $��43��6�   $��20(�6�   $��1��6�   �� 51      -   5        $ btn_addAlarm_Copy��51   !   $��	0,0,71,20q�6<   ���	#FFEAEAEA$��40��6f   $��Bottom=�6r   $��Right=�6�   $��43��6�   $��20(�6�   $��1��6�   ���	#FF487171���	#FF487171.��52        $��20��53      $��30��54      $	 TrashCanOutline=�55      $��Center=�64   $��Center=�6Q   56      57      58      �� 59      -   5        $ TabTimer��59      ��5:      �� 5;      $
��auto��5<      $
��auto��5=      .+�5>        $��24��5?      $��24��5@      $��Center=�5A      $	 TimerOutline=�5B      �� 5C      $��Center=�5D      $��Timer��5E      5F      .��5H      � 
 5I      �� 5J      $	��76* �6,   �� 5K      $
��585* �6,   .+�5N      �� $��
10,24,0,74q�6'   $��Visible=�6;   $��Hidden=�6a   $��2��6�   .��5O      � -   5        $ timer_container��5O   (   $
��Left=�6A   $
��1136��6\   5P      �� 5Q      -	   5        $ btn_addTimer��5Q   !   $��+��67   $��	0,0,16,20q�6C   ���	#FFEAEAEA$��40��6m   $��Bottom=�6y   $��Right=�6�   $��43��6�   $��20(�6�   $��1��6�   �� 5R      -
   5        $ btn_deleteTimer��5R   !   $��	0,0,71,20q�6:   ���	#FFEAEAEA$��40��6d   $��Bottom=�6p   $��Right=�6�   $��43��6�   $��20(�6�   $��1��6�   ���	#FF487171���	#FF487171.��5S        $��20��5T   !   $��30��5U   !   $	 TrashCanOutline=�5V   !   $��Center=�68   $��Center=�6U   5W      5X      5Y      �� 5Z      -   5        $ TabStopwatch��5Z      ��5[      �� 5\      $
��auto��5]      $
��auto��5^      .+�5_        $��24��5`      $��24��5a      $��Center=�5b      $	 TimerPlayOutline=�5c      �� 5d      $��Center=�5e      $��	Stopwatch��5f      5g      .��5i      � .+�5j      �� -   5        $ btn_stopWatch_play��5j   !   $
��Left=�6=   $��67��6X   $��475,558,0,0q�6d   $	��Top=�6y   $��84��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5k      V� 5l   "   .~�5m   &   �� �� 5n   &    �� AngleX$ 0.154Q�64   �� 5o   &    �� Angle$ -1.154Q�66   T� 5p   &   5q   #   .��5s        -   5          Name$ icon_btnStopWatch_play��5s   6   $��55��5t   !   $��55��5u   !   $
	 Play=�5v   !   
�������$��Center=�6@   $��Center=�6[   5w      �� 5x      -   5        $ btn_stopWatch_reset��5x   !   $
��Left=�6>   $��68��6Y   $��571,557,0,0q�6e   $	��Top=�6z   $��94��6�   .��5y        -   5        $
 sdf1��5y   6   $��55��5z   !   $��55��5{   !   $	 	Autorenew=�5|   !   
�������$��Center=�6E   $��Center=�6`   5}      �� 5~      -   5         ��Name$ label_stopWatchTime��5~   x   $��Center=�6    ${�False��6D   $��Center=�6V   $��00:00:00��6�   $
��Left=�6�   $	��162��6�   $��124,204,0,0q�6�   $	��Top=�6�   $	��880��6�   $��Verdana)�6	  $	��120(�6  5      5�      �� 5�      -   5        $ TabSettings��5�      ��5�      �� 5�      $
��auto��5�      $
��auto��5�      .+�5�        $��24��5�      $��24��5�      $��Center=�5�      $	 
CogOutline=�5�      �� 5�      $��Center=�5�      $��Settings��5�      5�      .��5�      � 
 5�      �� 5�      $��8* �6,   �� 5�      $
��157* �6,   .+�5�      d� -   5         d�Name$ tb_startOnWinStart��5�   '    $  MaterialDesignSwitchToggleButton#��[ 5�       � ToolTip$  Default ToggleButton Style��5�      $��	48,44,0,0q�6(   $
��Left=�6�   $��34��6�   $��28��6�   $	��Top=�6�   $��1��6�   �� 5�      $'��!run application on windows start?��6    $
��Left=�6L   $��42,9,0,0q�6g   $	��Top=�6y   $ 0.186,0.158&�6�   $��1��6�   $��18(�6�   ���	#DD6B6B6B6�   )  #MaterialDesignThemes.Wpf.TimePicker  5�      -   5          Name$ tp_toastNtf_expireTime��5�      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Time��5�         	Is24Hours$
 True��5�         WithSeconds$
 True��5�      $��
48,122,0,0q�60   $	��Top=�6D   $��34��6\      SelectedTime 03/04/2024 00:05:006h   $
��Left=�6�   $	��210��6�   $��1��6�   �� 5�      -   5         ��Name$! settings_slider_alarmVolume��5�   !   $
��Left=�6F   $��27��6a   $��
48,251,0,0q�6m   $	��Top=�6�   $	��464��6�    � Maximum$ 1Q�6�   $��1��6�   �� 5�      $'��!alarm notification (expire time):��6    $
��Left=�6L   $��	42,87,0,0q�6g   $	��Top=�6z   $ 0.186,0.158&�6�   $��1��6�   $��18(�6�   ���	#DD6B6B6B6�   �� 5�      $��alarm/timer sound volume:��6    $
��Left=�6D   $��
42,199,0,0q�6_   $	��Top=�6s   $ 0.186,0.158&�6�   $��1��6�   $��18(�6�   ���	#DD6B6B6B6�   �� 5�      $��Notes:��6    $
��Left=�61   $��
48,283,0,0q�6L   $	��Top=�6`   $ 0.186,0.158&�6x   $��1��6�   $��18(�6�   ���	#DD6B6B6B6�   �� 5�      $"��Notification not popping up?��6    $
��Left=�6G   $��
78,317,0,0q�6b   $	��Top=�6v   $ 0.186,0.158&�6�   $��1��6�   $��16(�6�   ���	#DD6B6B6B6�   �� 5�      $
��Left=�6$   $��39��6?   $��108,357,0,0q�6K   $��1��6`   $
��Wrap=�6m   $����- if so notifcations for this application is disabled, search "notification" in the windows search bar and click "Notifications & actions settings", find "ClockPlus" and switch/enable notification for the application, then try playing a timer to see if it works��6�   $	��Top=�6�  $	��813��6�  $��14(�6�  ���	#DD8C8C8C6�  �� 5�      $��Facing any bugs/issues?��6    $
��Left=�6B   $��
78,415,0,0q�6]   $	��Top=�6q   $ 0.186,0.158&�6�   $��1��6�   $��16(�6�   ���	#DD6B6B6B6�   �� 5�      $
��Left=�6$   $��39��6?   $��108,455,0,0q�6K   $��1��6`   $
��Wrap=�6m   $����- I really tested this application alot in various ways before, but if you encounter any, try commenting on the video and I will look into it��6�   $	��Top=�6  $	��813��6.  $��14(�6:  ���	#DD8C8C8C6H  �� 5�      $��Why I made this software?��6    $
��Left=�6D   $��
78,498,0,0q�6_   $	��Top=�6s   $ 0.186,0.158&�6�   $��1��6�   $��16(�6�   ���	#DD6B6B6B6�   �� 5�      $
��Left=�6$   $��55��6?   $��108,538,0,0q�6K   $��1��6`   $
��Wrap=�6m   $����- I'm a guy who spended most of his life heating up the chair, coding, playing games, trying to make games, modding them, and I have other many tasks to do besides my time on the chair, so I made this software so I can alarm and remind myself of various tasks ��6�   $	��Top=�6�  $	��813��6�  $��14(�6�  ���	#DD8C8C8C6�  r� 5�      -   5         r�Name$ settings_mediaElement��5�   '    r� Source$7 1Images/giphy1-ezgif.com-webp-to-gif-converter.gif?�6F    r� LoadedBehavior$
 Play=�6�   $y�Visible=�6�   $��773,31,49,297q�6�   $��2��6�    r� UnloadedBehavior$ Manual=�6�   6�   5�      5�      5�      5�      5�      5�      