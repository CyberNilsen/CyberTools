   M S B A M L   `   `   `  ����E  ACyberTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CyberTools
CyberTools  5   	   ;"clr-namespace:CyberTools.ViewModelCyberTools.ViewModel  5   	       CyberTools.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CyberTools     5   	   8	viewmodel"clr-namespace:CyberTools.ViewModel     5   	     � Title$  
CyberTools��5	   	   $	��800��6   $
��1200��6)    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$2 ,pack://application:,,,/Images/CyberTools.ico��5   	   .��5      �� 
������� �� CornerRadius$ 20��6     J� Clip 5   
   �� 5       �� Rect$ 0,0,1200,800�6    	 �� RadiusX$	 15Q�64   
 �� RadiusY$
 15Q�6A   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��250 �6#   �� 5       � RowDefinitions 5      �� 5      $	��200 �6    .+�5"      � $��0��6   
����  �$��
0,0,0,-600q�65   �� 5$      -   5        $
��Main��5$      $��70��6!   $��	Main page��6-   $��
0,100,0,30q�6A   $��22(�6h   �� 5%      -   5        $��	Keylogger��5%      $��70��6&   $��	Keylogger��62   $��0,170,0,-40q�6F   $��22(�6s   �� 5&      -   5        $
��Wifi��5&      $��70��6!   $��Wifi deauthenticate��6-   $��0,240,0,-110q�6K   $��22(�6t   � 5(      $��1��6   $��Right=�6#   $	��Top=�6?   $��	0,10,10,0q�6W   .+�5)      �� -   5         ��Name$ CloseButton��5)      $��X��6.   $��30��6:   $��30��6E   $��Right=�6k   $	��Top=�6�   ��5*      �� 5+       �� 
TargetType" ��6    .�5,      ��  �� Property � �� �� Value 5-   "   �� 5.   &    �� 
TargetType" ��66   .R�5/   *   ��  ��Name$ border��61   #��z��50   )   #��z��51   )   $��1q�52   )   $ 9��53   )   ._�54   .   �� $��Center=�6?   $��Center=�6\   55   +    �� Triggers 56   *   P� 57   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �58   2   ��  �� 
TargetName$ border��69    � ��
 �  ��6c   �� 59   2    � ��
 �����6O   5:   /   5<   '   5>      �� 5@       � ��
 �����6;   �� 5A       � ��
 �   �6;   �� 5B       � ��
 ���� 6<   �� 5C       � ��$ 0q�6@   �� 5D       � ��$ 20(�69   5E      5G      5H      � 5J      $��1��6   $��Right=�6#   $	��Top=�6?   $��	0,10,50,0q�6W   .+�5K      �� -   5        $��-��5K      $��30��6%   $��30��60   $��
10,0,-10,0q�6Y   ��5L      �� 5M      " ��6    .�5N      ��  � �� 5O   "   �� 5P   &   " ��66   .R�5Q   *   �� $ border��61   #��z��5R   )   #��z��5S   )   $��1q�5T   )   $ 9��5U   )   ._�5V   .   �� $��Center=�6?   $	��Top=�6\   5W   +    5X   *   P� 5Y   .    �@J�IsMouseOver$
 True��6M   . �5Z   2   �� $ border��69    � ��
 �����6c   �� 5[   2    � ��
 �����6O   5\   /   5^   '   5`      �� 5b       � ��
 �����6;   �� 5c       � ��
 �   �6;   �� 5d       � ��
 ���� 6<   �� 5e       � ��$ 0q�6@   �� 5f       � ��$ 24(�69   5g      5i      5j      5k      5l      5m      