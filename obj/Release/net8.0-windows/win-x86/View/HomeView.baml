   M S B A M L   `   `   `  ����   
CyberTools    CyberTools.View.HomeView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� .+�� .+��� $��750px��$��500px��$��Center=�$��Center=�  �� CornerRadius$  15������ -   AnimatedGradient�� �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.��� -   GradientStop1�� � Color$ #007bff�� � Offset$ 0Q�� -   GradientStop2��$ #6200EE��$ 1Q�._�� $��Center=�$��Center=� � RowDefinitions �� $
��auto ��� $��* �.+��� $��
CyberTools��$��Center=�$��Center=�$��50(�
�������$��0���� $	��350��$	��350��$��/Images/Mr-robot.jpg��$��1��$��0,10,0,0q� � Triggers :�  :� RoutedEvent$ Border.Loaded��.[��� .����  g� RepeatBehavior$ Forever��	 g� AutoReverse$
	 True��.���� 
 �� 
TargetName$
 GradientStop1�� �� TargetProperty$ Color� �� From #007bff	 �� To #ff00ff g� Duration$ 0:0:50D�