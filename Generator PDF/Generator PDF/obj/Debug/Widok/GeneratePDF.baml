   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      H DGenerator PDF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Generator_PDFGenerator_PDF 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	      Generator_PDF.GeneratePDF   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      -localclr-namespace:Generator_PDF     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      -   5          � Title$  GeneratePDF��5
   	   $	��400��6   $	��710��6*    � WindowStyle$
 None=�66   ���	#FF2D2D30 � 
ResizeMode$ NoResize=�5   !    � WindowStartupLocation$ CenterScreen=�67   .��5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��320.8 �6   �� 5      $
��23.2 �6    � ColumnDefinitions 5   
   �� 5      $
��280* �6   �� 5      $
��280* �6   �� 5      $
��150* �6   .+�5   
   � $��0��6   $��0��6   .+�5       (  "System.Windows.Controls.DatePicker  $	��150��6   ���	#FF333337���	#FF7AC1FF   SelectedDate 5#   -   ��  �� Path$ AvailableFrom� �� Mode$ TwoWay=�
	 �� Now
 �� TargetNullValue#
 Z	 5&      5'      � 5(   
   $��0��6   $��1��6   .+�5)        $	��150��6   ���	#FF333337���	#FF7AC1FF 5,      �� $ AvailableTo�$ TwoWay=�#
 Z	 5/      50      � 53   
   $��0��6   $��3��6   .+�54      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��1��6N   .+�55      �� $��49��6   $��40��6$   
������ 
������ $	��Top=�6d   $��
0,0,0,-5.6q�6�   ��6|   �� *CloseButtonCommand+.��56      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�6.   ���	#FFCCCCCD6;   57      58      59      � 5:   
   $��1��6   .+�5=      �� -   5         ��Name$ 
selectList��5=      ���	#FFF1F1F1���	#FF333337$
��auto��6X   $	��250��6f   ��5?      �� *possiblyOperationsListBox+��5B      �� 5C      .R�5E      �� $��2q�6C   $
��Bold"�6N   
�������$��WrapWithOverflow=�6t   ��6(   �� *	operation+6t   5G      5J      5L      � 5M   
   $��1��6   $��1��6   .+�5O      �� -   5        $ mainList��5O      ���	#FFF1F1F1���	#FF333337$
��auto��6V   $	��250��6d   ��5Q      �� *operationsListBox+��5T      �� 5U      .R�5W      �� $��2q�6C   $
��Bold"�6N   
�������$
��Wrap=�6t   ��6(   �� *	operation+6t   5Y      5\      5^      � 5_   
   $��2��6   $��1��6    5`      �� 5a      $	��54* �6    �� 5b      $	��54* �6    �� 5c      $	��54* �6    �� 5d      $
��137* �6    .+�5f      � $��0��6   .+�5g      �� $
��auto��6   $��54��6&   $��Center=�5h      
������ 
������ $	��Add��5i   /   $��15(�5j      ���	#FFF1F1F1��5k      �� *AddOperationButtonCommand+5l      5m      � 5n      $��1��6   .+�5o      �� $
��auto��6   $��54��6&   $��Center=�5p      
������ 
������ $��Remove��5q   /   $��15(�5r      ���	#FFF1F1F1��5s      �� *RemoveButtonCommand+5t      5u      � 5v      $��2��6   .+�5w      �� $
��auto��6   $��54��6&   $��Center=�5x      
������ 
������ $��Generate PDF��5y   /   $��15(�5z      ���	#FFF1F1F1��5{      �� *GeneratePdfButtonCommand+5|      5}      5�      5�      5�      