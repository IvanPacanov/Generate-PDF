   M S B A M L   `   `   `  ����H  DGenerator PDF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Generator_PDFGenerator_PDF  5   	       Generator_PDF.LogIn   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Generator_PDF     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          � Title$  LogIn��5	   	   $	��400��6   $	��400��6$    � WindowStyle$
 None=�60   ���	#FF2D2D30 � 
ResizeMode$ NoResize=�5
   !    � WindowStartupLocation$ CenterScreen=�67   .��5      .��5      �  � RowDefinitions 5   
   �� 5      $	��46* �6   �� 5      $
��242* �6   �� 5      $	��80* �6   .+�5   
   � $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5      �� $��49��6   $��40��6$   
������ 
������ $	��Top=�6d   $��
0,0,0,-5.6q�6�   ��6|   �� *CloseButtonCommand+.��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�6.   ���	#FFCCCCCD6;   5      5       5!      � 5#   
   $��1��6   .+�5$      �� $��Center=�6   $��10q�64   .+�5%      �� -   5         ��Name$ Ip��5%      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ IP��6&    %  !MaterialDesignFloatingHintTextBox#��[  6J   $
��0,10q�6�   $��High Tower Text)�6�   $��18(�6�   ���	#FFF1F1F1	 �� 
CaretBrush	 �	#FF7AC1FF��6�   �� *TextIp+6�   �� 5&      $ Login��6   #��[  6B   $
��0,10q�6}   $��High Tower Text)�6�   $��18(�6�   ���	#FFF1F1F1	 �	#FF7AC1FF��6�   �� *	TextLog+6�   G� 5'      -   5        
 G�Name$
 Password��5'   �   $ Password��6    ) %MaterialDesignFloatingHintPasswordBox#��[ 6J   $
��0,10q�6�   $��High Tower Text)�6�   $��18(�6�   ���	#FFF1F1F1 G� 
CaretBrush �	#FF7AC1FF6�   �� 5(      -   5        $ BaseData��5(      $ Database��6,   #��[  6V   $
��0,10q�6�   $��High Tower Text)�6�   $��18(�6�   ���	#FFF1F1F1	 �	#FF7AC1FF��6�   �� *TextDataBase+6�   5*      5+      � 5-   
   $��3��6   .+�5.      �� $	��120��6   $��40��6!   $��Center=�5/      
������ 
������ $��
Connection��50   /   $��15(�51      ���	#FFF1F1F1��52      �� *ConnectionButtonCommand+53      55      57      58      