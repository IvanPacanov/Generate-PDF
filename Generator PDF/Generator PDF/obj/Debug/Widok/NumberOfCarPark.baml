   M S B A M L   `   `   `  ����H  DGenerator PDF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Generator_PDFGenerator_PDF  5   	   #    Generator_PDF.NumberOfCarPark   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Generator_PDF     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          � Title$  CarPark��5	   	   $	��400��6   $	��710��6&    � WindowStyle$
 None=�62   ���	#FF2D2D30 � 
ResizeMode$ NoResize=�5
   !    � WindowStartupLocation$ CenterScreen=�67   .��5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��320.8 �6   �� 5      $
��23.2 �6    � ColumnDefinitions 5   
   �� 5      $
��280* �6   �� 5      $
��280* �6   �� 5      $
��150* �6   .+�5!   
   � $��0��6   $��3��6   .+�5"      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��1��6N   .+�5#      �� $��49��6   $��40��6$   
������ 
������ $	��Top=�6d   $��
0,0,0,-5.6q�6�   ��6|   �� *CloseButtonCommand+.��5$      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�6.   ���	#FFCCCCCD6;   5%      5&      5'      � 5(   
   $��1��6   .+�5+      �� -   5         ��Name$ 
selectList��5+      ���	#FFF1F1F1���	#FF333337$
��auto��6X   $	��250��6f   ��5-      �� *possiblyParkingListBox+��5/      �� 50      .R�51      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�52      �� $��2q�6=   $
��Bold"�6H   
�������$
��Wrap=�6m   ��6'   �� *name+6m   53      54      57      59      � 5:   
   $��1��6   $��1��6   .+�5<      �� -   5        $ mainList��5<      ���	#FFF1F1F1���	#FF333337$
��auto��6V   $	��250��6d   ��5>      �� *carParksListBox+��5A      �� 5B      .R�5C      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5D      �� $��2q�6>   $
��Bold"�6I   
�������$
��Wrap=�6o   ��6(   �� *name+6o   5E      5F      5I      5K      � 5L   
   $��2��6   $��1��6    5M      �� 5N      $	��54* �6    �� 5O      $	��54* �6    �� 5P      $	��54* �6    �� 5Q      $
��137* �6    .+�5S      � $��0��6   .+�5T      �� $
��auto��6   $��54��6&   $��Center=�5U      
������ 
������ $	��Add��5V   /   $��15(�5W      ���	#FFF1F1F1��5X      �� *AddCarParkButtonCommand+5Y      5Z      � 5[      $��1��6   .+�5\      �� $
��auto��6   $��54��6&   $��Center=�5]      
������ 
������ $��Remove��5^   /   $��15(�5_      ���	#FFF1F1F1��5`      �� *RemoveCarParkButtonCommand+5a      5b      � 5c      $��2��6   .+�5d      �� $
��auto��6   $��54��6&   $��Center=�5e      
������ 
������ $��Continue��5f   /   $��15(�5g      ���	#FFF1F1F1��5h      �� *ContinuePdfButtonCommand+5i      5j      5m      5n      5p      