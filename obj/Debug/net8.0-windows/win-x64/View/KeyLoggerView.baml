   M S B A M L   `   `   `  ����E  ACyberTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CyberTools.ViewCyberTools.View  5      #    CyberTools.View.KeyLoggerView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:CyberTools.View     5      
�������.��5      �� $��850px��6   $��500px��5      $��Center=�5      $��Center=�5      ._�5   
   � .+�5      �� $��	KeyLogger��6   
���   �$��50(�6<   $��Center=�6J   $	��Top=�6g   $��0,58,0,0q�6   �� 5      -   5        $��Start KeyLogger��5      $��Center=�6L   $	��Top=�6j   $��66��6�   $	��180��6�   $��	0,185,0,0q�6�   ��5      �� 5        �� 
TargetType"  ��6&   .R�5      ��  ��Name$ border��6!   #��z��61   #��z��5      $��1q�5       �� CornerRadius$ 5��6)   ._�5      �� $��Center=�6/   $��Center=�6L   5        �� Triggers 5!      P� 5"       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �5#   "   ��  �� 
TargetName$ border��6)    �� Property � �� �� Value
 ��� �6S   �� 5$   "    � ��
 �   �6?   5'      5)      ��5+      �� 5,      	 �� 
TargetType"	 ��6   .�5-      ��  � ��
 �����67   �� 5.       � ��
 �   �67   �� 5/       � ��$ 16(�65   �� 50       � ��
 �   �68   �� 51       � ��$ 1q�6<   �� 52       � ��$ 5q�63   �� 53       � ��$ 10q�64   �� 54       �@J�Effect 55      3  -System.Windows.Media.Effects.DropShadowEffect  56   "   
   
BlurRadius$
 10Q�63      Color$ Black��6C      Opacity$	 0.3Q�6Q   58      5:      5=      �� 5>      -   5        $��End KeyLogger��5>      $��Center=�6H   $	��Top=�6f   $��66��6~   $	��180��6�   $��	0,276,0,0q�6�   ��5?      �� 5@      "  ��6&   .R�5A      �� $ border��6!   #��z��61   #��z��5B      $��1q�5C      $ 5��6-   ._�5D      �� $��Center=�6/   $��Center=�6L   5E       5F      P� 5G       �@J�IsMouseOver$
 True��6=   . �5H   "   �� $ border��6)    � ��
 �  ��6S   �� 5I   "    � ��
 �   �6?   5J      5L      ��5N      �� 5O      "	 ��6   .�5P      ��  � ��
 �����67   �� 5Q       � ��
 �   �67   �� 5R       � ��$ 16(�65   �� 5S       � ��
 �   �68   �� 5T       � ��$ 1q�6<   �� 5U       � ��$ 5q�63   �� 5V       � ��$ 10q�64   �� 5W       �@J�Effect 5X        5Y   "   $
 10Q�63   $ Black��6C   $	 0.3Q�6Q   5[      5\      5_      5b      5c      5e      