   M S B A M L   `   `   `  ����E  ACyberTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CyberTools
CyberTools  5   	   ;"clr-namespace:CyberTools.ViewModelCyberTools.ViewModel  5   	       CyberTools.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CyberTools     5   	   8	viewmodel"clr-namespace:CyberTools.ViewModel     5   	     � Title$  
CyberTools��5	   	   $	��600��6   $
��1000��6)    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ 	CanResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ /Images/CyberTools.ico��5   	    � DataContext 5      (  "CyberTools.ViewModel.MainViewModel  5   
   .��5      �� 
������� �� CornerRadius$ 20��6    ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      	 � RowDefinitions	 5!      �� 5"      $��75 �6    �� 5#      .+�5&      � $��0��6   $��1��6    $��Center=�60   $	��Top=�6M   $	��530��6e   $��66��6q   $��0,4,70,0q�6}   .+�5'      �� -   5        $��-146,-3,-99,-3q�5'   O   5(      � 5*      $��0��6   $��1��6    $��Right=�60   $	��Top=�6L   $��	0,10,10,0q�6d   .+�5+      �� -   5        
 ��Name$
 CloseButton��5+      $��X��6.   $��30��6:   $��30��6E   $��Right=�6k   $	��Top=�6�   ��5,      �� 5-       �� 
TargetType" ��6    .�5.      ��  �� Property � �� �� Value 5/   "   �� 50   &    �� 
TargetType" ��66   .R�51   *   ��  ��Name$ border��61   #��z��52   1   #��z��53   1   $��1q�54   1   $ 3��55   1   ._�56   .   �� $��Center=�6?   $��Center=�6\   57   +    �� Triggers 58   *   P� 59   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �5:   2   ��  �� 
TargetName$ border��69    � ��
 �  ��6c   �� 5;   2    � ��
 �����6O   5<   /   5>   '   5@      �� 5B       � ��
 �����6;   �� 5C       � ��
 �   �6;   �� 5D       � ��
 �����6<   �� 5E       � ��$ 0q�6@   �� 5F       � ��$ 20(�69   5G      5I      �� 5K      -   5        $��-��5K      $��30��6%   $��30��60   $��Right=�6Y   $	��Top=�6u   $��	55,0,30,0q�6�   ��5L      �� 5M      " ��6    .�5N      ��  � �� 5O   "   �� 5P   &   " ��66   .R�5Q   *   �� $ border��61   #��z��5R   1   #��z��5S   1   $��1q�5T   1   $ 3��5U   1   ._�5V   .   �� $��Center=�6?   $	��Top=�6\   5W   +    5X   *   P� 5Y   .    �@J�IsMouseOver$
 True��6M   . �5Z   2   �� $ border��69    � ��
 �����6c   �� 5[   2    � ��
 �����6O   5\   /   5^   '   5`      �� 5b       � ��
 �����6;   �� 5c       � ��
 �   �6;   �� 5d       � ��
 �����6<   �� 5e       � ��$ 0q�6@   �� 5f       � ��$ 24(�69   5g      5i      5j      �� 5l      -   5        $��
CyberTools��5l      $��Center=�5m      $��Center=�5n      
���   �$��22(�5p      $��Segoe UI)�5q      5r      �� 5t      $��1��6   .+�5u      �� $
��Home��6   $��66��6(   
���   �$��14(�6G   ��6U   �� *HomeViewCommand+��5v      �� 5w      " ��6*   .R�5x      �� $ border��6%   #��z��65   #��z��5y   %   $��1q�5z   %   $ 5��69   ._�5{   "   �� $��Center=�63   $��Center=�6P   5|       5~      P� 5   "    �@J�IsMouseOver$
 True��6A   . �5�   &   �� $ border��6-    � ��
 ��� �6W   �� 5�   &    � ��
 �����6C   5�   #   5�      ��5�      �� 5�      " ��6    .�5�      ��  � ��
 �����6;   �� 5�       � ��
 �   �6;   �� 5�       � ��$ 14(�69   �� 5�       � ��
 �   �6<   �� 5�       � ��$ 1q�6@   �� 5�       � ��$ 5q�67   �� 5�       � ��$ 10q�68   �� 5�       �@J�Effect 5�   "   3  -System.Windows.Media.Effects.DropShadowEffect  5�   &      
BlurRadius$ 10Q�67      Color$ Black��6G      Opacity$	 0.3Q�6U   5�      5�      5�      �� 5�      $��
Key Logger��6   $��66��6.   
���   �$��14(�6M   ��6[   �� *KeyloggerViewCommand+��5�      �� 5�      " ��6*   .R�5�      �� $ border��6%   #��z��65   #��z��5�   %   $��1q�5�   %   $ 5��69   ._�5�   "   �� $��Center=�63   $��Center=�6P   5�       5�      P� 5�   "    �@J�IsMouseOver$
 True��6A   . �5�   &   �� $ border��6-    � ��
 ��� �6W   �� 5�   &    � ��
 �����6C   5�   #   5�      ��5�      �� 5�      " ��6    .�5�      ��  � ��
 �����6;   �� 5�       � ��
 �   �6;   �� 5�       � ��$ 14(�69   �� 5�       � ��
 �   �6<   �� 5�       � ��$ 1q�6@   �� 5�       � ��$ 5q�67   �� 5�       � ��$ 10q�68   �� 5�       �@J�Effect 5�   "     5�   &   $ 10Q�67   $ Black��6G   $	 0.3Q�6U   5�      5�      5�      5�      �� 5�      $��1��6   $��1��6*   $��10q�6:   ��6F   �� *CurrentView+5�      5�      5�      