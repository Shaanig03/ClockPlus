   M S B A M L   `   `   `  ���� D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus      ClockPlus.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:ClockPlus     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  
Clock Plus��$	��676��$
��1234��   MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(� � Icon$ 
/icon2.png�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=���:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��� .+�#  MaterialDesignThemes.Wpf.Card  .���� -    ��Name$ 
tabControl��3  -MaterialDesignThemes.Wpf.NavigationRailAssist   ShowSelectionBackground$
 True�� J� SnapsToDevicePixels$
 True�� * &MaterialDesignNavigationRailTabControl#��[  �� TabStripPlacement$
 Left=�.@��� -    ��Name$ TabAlarm������ $
��auto��$
��auto��.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��24��$��24��$��Center=�	   Kind$	 ProgressClock=��� $��Center=�$��Alarm��.��� 
 � RowDefinitions
 �� $	��76* ��� $
��585* �.+��� $��
10,24,0,74q�$��Visible=�$��Hidden=�$��2��.��� -    �Name$ alarm_container��$
��Left=�$
��1136���� -    ��Name$ btn_addAlarm��$��+��$��	0,0,16,20q����	#FFEAEAEA$��40��$��Bottom=�$��Right=�$��43��$��20(�$��1���� -   $ btn_addAlarm_Copy��$��	0,0,71,20q����	#FFEAEAEA$��40��$��Bottom=�$��Right=�$��43��$��20(�$��1�����	#FF487171���	#FF487171.��  $��20��$��30��$	 TrashCanOutline=�$��Center=�$��Center=��� -   $ TabTimer������ $
��auto��$
��auto��.+�  $��24��$��24��$��Center=�$	 TimerOutline=��� $��Center=�$��Timer��.��� 
 �� $	��76* ��� $
��585* �.+��� $��
10,24,0,74q�$��Visible=�$��Hidden=�$��2��.��� -   $ timer_container��$
��Left=�$
��1136���� -	   $ btn_addTimer��$��+��$��	0,0,16,20q����	#FFEAEAEA$��40��$��Bottom=�$��Right=�$��43��$��20(�$��1���� -
   $ btn_deleteTimer��$��	0,0,71,20q����	#FFEAEAEA$��40��$��Bottom=�$��Right=�$��43��$��20(�$��1�����	#FF487171���	#FF487171.��  $��20��$��30��$	 TrashCanOutline=�$��Center=�$��Center=��� -   $ TabStopwatch������ $
��auto��$
��auto��.+�  $��24��$��24��$��Center=�$	 TimerPlayOutline=��� $��Center=�$��	Stopwatch��.��� .+��� -   $ btn_stopWatch_play��$
��Left=�$��67��$��482,558,0,0q�$	��Top=�$��84�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� ��  �� AngleX$ 0.154Q���  �� Angle$ -1.154Q�T� .��  -     Name$ icon_btnStopWatch_play��$��55��$��55��$
	 Play=�
�������$��Center=�$��Center=��� -   $ btn_stopWatch_reset��$
��Left=�$��68��$��578,557,0,0q�$	��Top=�$��94��.��  -   $
 sdf1��$��55��$��55��$	 	Autorenew=�
�������$��Center=�$��Center=��� -    ��Name$ label_stopWatchTime��$��Center=�$��Center=�$��00:00:00��$
��Left=�$	��162��$��124,204,0,0q�$	��Top=�$	��880��$��Verdana)�$	��120(��� -   $ TabSettings������ $
��auto��$
��auto��.+�  $��24��$��24��$��Center=�$	 
CogOutline=��� $��Center=�$��Settings��.��� .+�d� -    d�Name$ tb_startOnWinStart�� $  MaterialDesignSwitchToggleButton#��[  � ToolTip$  Default ToggleButton Style��$��
138,79,0,0q�$
��Left=�$��34��$��28��$	��Top=��� $.��(start this application on windows start?��$
��Left=�$��	46,52,0,0q�$	��Top=�$ 0.186,0.158&�