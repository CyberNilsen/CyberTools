   M S B A M L   `   `   `  ���� E  ACyberTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CyberTools
CyberTools  ;"clr-namespace:CyberTools.ViewModelCyberTools.ViewModel      CyberTools.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:CyberTools     8	viewmodel"clr-namespace:CyberTools.ViewModel       � Title$  
CyberTools��$	��600��$
��1000�� � WindowStyle$
 None=� � 
ResizeMode$ 	CanResize=�
������  � AllowsTransparency$
 True�� � WindowStartupLocation$ CenterScreen=� � Icon$2 ,pack://application:,,,/Images/CyberTools.ico�� � DataContext (  "CyberTools.ViewModel.MainViewModel  .���� 
������� �� CornerRadius$ 20��._��  � ColumnDefinitions �� $	��200 ��� 	 � RowDefinitions	 �� $��75 ��� .+�� $��0��$��1��$��Center=�$	��Top=�$	��530��$��66��$��0,4,70,0q�.+��� -   $��-146,-3,-99,-3q�� $��0��$��1��$��Right=�$	��Top=�$��	0,10,10,0q�.+��� -   
 ��Name$
 CloseButton��$��X��$��30��$��30��$��Right=�$	��Top=�����  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R���  ��Name$ border��#��z��#��z��$��1q�$ 3��._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� 
TargetName$ border�� � ��
 �  ����  � ��
 �������  � ��
 �������  � ��
 �   ���  � ��
 �������  � ��$ 0q���  � ��$ 20(��� -   $��-��$��30��$��30��$��Right=�$	��Top=�$��	55,0,30,0q����� " ��.���  � �� �� " ��.R��� $ border��#��z��#��z��$��1q�$ 3��._��� $��Center=�$	��Top=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � ��
 �������  � ��
 �������  � ��
 �������  � ��
 �   ���  � ��
 �������  � ��$ 0q���  � ��$ 24(��� -   $��
CyberTools��$��Center=�$��Center=�
���   �$��22(�$��Segoe UI)��� $��1��.+��� $
��Home��$��66��
���   �$��14(����� *HomeViewCommand+���� " ��.R��� $ border��#��z��#��z��$��1q�$ 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � ��
 ��� ���  � ��
 ��������� " ��.���  � ��
 �������  � ��
 �   ���  � ��$ 14(���  � ��
 �   ���  � ��$ 1q���  � ��$ 5q���  � ��$ 10q���  �@J�Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   Color$ Black��   Opacity$	 0.3Q��� $��
Key Logger��$��66��
���   �$��14(����� *KeyloggerViewCommand+���� " ��.R��� $ border��#��z��#��z��$��1q�$ 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � ��
 ��� ���  � ��
 ��������� " ��.���  � ��
 �������  � ��
 �   ���  � ��$ 14(���  � ��
 �   ���  � ��$ 1q���  � ��$ 5q���  � ��$ 10q���  �@J�Effect   $ 10Q�$ Black��$	 0.3Q��� $��1��$��1��$��10q����� *CurrentView+