   M S B A M L   `   `   `  ���� D  @ClockPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClockPlus	ClockPlus       ClockPlus.TimerItemControl   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:ClockPlus     _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  $	��330��$	��354��   MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(���:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .���   �� EdgeMode$  Unspecified=� �� BitmapScalingMode$ NearestNeighbor=� � UseLayoutRounding$
 True��.+��� -    ��Name$ border1�� �� CornerRadius$ 6��$��0.8                 q�$��Center=�$	��324��$��Center=�$	��342�����	#FFADADAD
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$
 0.08Q�._�� .+�� -    �Name$ circularProgressBar�� % !MaterialDesignCircularProgressBar#��[ $��80��$��80�� � SmallChange$ 1Q�	 J� RenderTransformOrigin$		 0,0&�
 � Value$	
 100Q�z�V� .~��� �� �� T�  T� Y$ -1Q� � LayoutTransform ��  �� ScaleX$	 2.7Q� �� ScaleY$	 2.7Q��� -    ��Name$ l_time��$��Center=�$��Center=�$��Center=�$��00:00:00��$	��340��$��35(�$��Verdana)�$��	0,85,0,85q����	#FF424242���	#FF08A753�� -   $ 
l_cmdCount��$|�False��$��Center=�$��Center=�$��0 cmd��$��Center=�$��26��$��	0,196,0,0q�$	��Top=�$��44��$��Segoe UI)�$��Normal"����	#FF868686$��11(��� -   $ l_title��$��Timer Item Name��$
��Left=�$��23��$��	10,10,0,0q�$	��Top=�$	��328����Z� �� $��1q�$��Center=�$	��322��$��Center=�$	��340�����	#02FFFFFF._�%  MaterialDesignThemes.Wpf.Ripple  -     Name$ ripple��$
{�True��$��8q�$��Center=�$��Center=�$|�False�� J� SnapsToDevicePixels$
 True��$��	0,0,-2,-2q��� $��1q�$
��Left=�$��42��$��122,273,0,0q�$	��Top=�$	��105�����	#02FFFFFF._�� .+��� -    ��Name$ btn_play��$
��Left=�$��37��$��2,0,0,0q�$��Center=�$��44��.��'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ icon_btnPlay��$��25��$��24��   Kind$
 Play=�
�������$��Center=�$��Center=��� -	   $ 	btn_reset��$
��Left=�$��37��$��52,0,0,0q�$��Center=�$��50��.��  -
   $
 sdf1��$��26��$��26��$ 	Autorenew=�
�������$��Center=�$��Center=��� $��1q�$
��Left=�$��30��$��
293,10,0,0q�$	��Top=�$��33�����	#02FFFFFF._�d� -    d�Name$ 
delete_btn��$y�Hidden=� " MaterialDesignFlatToggleButton#��[  � ToolTip$ Deletes Alarm�����	#FFD04300���	#FFD04300���	#FFD04300$��
0,-6,-4,-6q�$��Right=�$��40��.��  -   $ ___No_Name_��$��24��$��25��$ DeleteForever=����	#FFD04300$��Center=�