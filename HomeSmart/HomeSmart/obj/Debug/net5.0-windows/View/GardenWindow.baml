   M S B A M L   `   `   `  ����D  @HomeSmart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:HomeSmart.ViewHomeSmart.View  5   	   G(clr-namespace:HomeSmart.UserControlKteamHomeSmart.UserControlKteam  5	   N   !    HomeSmart.View.GardenWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:HomeSmart.View     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   1uc(clr-namespace:HomeSmart.UserControlKteam  6N   -   5        $��garden��5   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�5   	    � WindowStartupLocation$ CenterScreen=�5   	     GardenVM � DataContext# [ 5   	    � Title$ Vườn��5   	   $	��450��6   $	��800��6#   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      -   'HomeSmart.UserControlKteam.ControlBarUC  
	 � Tag	 6   �� *Title+
 �� ElementName$
 garden��6K   5      � 5    
   $��1��6    5!      �� 5"      $
��auto �6    �� 5#      $
��Auto �6    .+�5%      � $��0��6   .+�5&      #  MaterialDesignThemes.Wpf.Card  $��5q�6&   .��5'      � $��0��6    5(      �� 5)      $
��auto �6,   6<   �� 5*      $
��auto �6,   6<   .+�5,      � $��0��6   .+�5-      �� $��5q�6(   65   5.      � 5/      $��1��6    � ColumnDefinitions 50      �� 51   "   66   �� 52   "   66   .+�55      � $��0��6#   .+�56   "     ���#03a9f4$��5q�6L    ! PrimaryHueDarkForegroundBrush#���  57   :   $��0q�58   :   .��59   &   �  5:   *   �� 5;   .   $
��Auto �6<   �� 5<   .   $
��Auto �6<   �� 5=   .   $
��Auto �6<   .+�5?   *   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 	PowerPlug=�6B   �� 5@   *   $��0��64   $��
16 16 16 4q�6A   $��Độ ẩm đất��5A   8      MaterialDesignBody1TextBlock#��[ 5B   8   5D   +   �� 5E   *   $��1��64      MaterialDesignLightSeparator#��[ 6A   � 5F   *   $��2��6/    5G   .   �� 5H   2   .+�5J   .   �� $��0��68   $��	16 0 16 8q�6H   $��Center=�6[   $
��Left=�5K   8   #��[ 5M   8   ��5L   8   �� *
Humidity+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5N   /   5O   +   '  !MaterialDesignThemes.Wpf.PopupBox  5P   *   $��Right=�6B   $��2 0 2 0q�6^      PopupContent. 5Q   .   �� 5S   /   5T   +   5U   '   5V   #   5W      � 5Y      $��1��6#   .+�5Z   "     ���#03a9f4$��5q�6L   #���  5[   :   $��0q�5\   :   .��5]   &   �  5^   *   �� 5_   .   $
��Auto �6<   �� 5`   .   $
��Auto �6<   �� 5a   .   $
��Auto �6<   .+�5c   *     $ 	PowerPlug=�6B   �� 5d   *   $��0��64   $��
16 16 16 4q�6A   $��Nhiệt độ��5e   8   #��[ 5f   8   5h   +   �� 5i   *   $��1��64   #��[ 6A   � 5j   *   $��2��6/    5k   .   �� 5l   2   .+�5n   .   �� $��0��68   $��	16 0 16 8q�6H   $��Center=�6[   $
��Left=�5o   8   #��[ 5q   8   ��5p   8   �� *Temp+$ TwoWay=�$ PropertyChanged=�5r   /   5s   +     5t   *   $��Right=�6B   $��2 0 2 0q�6^   . 5u   .   �� 5w   /   5x   +   5y   '   5z   #   5{      5|      5      5�      5�      � 5�      $��1��6   .+�5�        $��5q�6&   .��5�      � $��0��6    5�      �� 5�      $
��auto �6,   6<   �� 5�      $
��auto �6,   6<   .+�5�      � $��0��6   .+�5�      �� $��5q�6(   65   5�      � 5�      $��1��6    5�      �� 5�   "   66   �� 5�   "   66   .+�5�      � $��0��6#   .+�5�   "     ���#03a9f4$��5q�6L   #���  5�   :   $��0q�5�   :   .��5�   &   �  5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   �� 5�   .   $
��Auto �6<   .+�5�   *     $ 	PowerPlug=�6B   �� 5�   *   $��0��64   $��
16 16 16 4q�6A   $��Tưới cây��5�   8   #��[ 5�   8   5�   +   �� 5�   *   $��1��64   #��[ 6A   � 5�   *   $��2��6/    5�   .   �� 5�   2   .+�5�   .   �� $��0��68   $��	16 0 16 8q�6H   $��Center=�6[   $
��Left=�5�   8   #��[ 5�   8   ��5�   8   �� *	WaterPump+$ TwoWay=�$ PropertyChanged=�5�   /   5�   +     5�   *   $��Right=�6B   $��2 0 2 0q�6^   . 5�   .   �� 5�   /   5�   +   5�   '   5�   #   5�      � 5�      $��1��6#   .+�5�   "   �  5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   .+�5�   &   �� $��	0 5 150 0q�5�   -   $��0��5�   -    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 5�   -    � ToolTip$1 +MaterialDesignFloatingActionMiniLightButton��5�   -   ��5�   -   �� *OpenWaterPump+.��5�   *     $ Power=�5�   1   $��24��5�   1   $��24��5�   1   5�   '   �� 5�   &   $��1��60   #��[ 6=   �� 5�   &   $��	0 0 150 0q�5�   -   $��2��5�   -    / +MaterialDesignFloatingActionMiniLightButton#��[ 5�   -   $1 +MaterialDesignFloatingActionMiniLightButton��5�   -   ��5�   -   �� *CloseWaterPump+.��5�   *     $ PowerPlugOff=�6B   $��24��6V   $��24��6b   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      