   M S B A M L   `   `   `  ����C  ?GoodFood, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:GoodFood.ViewGoodFood.View  5      &     GoodFood.View.AllRestaurantsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Z VDevExpress.Mvvm.UI, Version=20.2.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:GoodFood.View     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignBody#���   5   
   $��Regular"�5	   
   $��13(�5
   
   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   
      TextRenderingMode$
 Auto=�5   
     MaterialDesignFont#���  5   
   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��
0,20,40,20q�6   $��Center=�6)   $��Center=�6D    �� Orientation$ 
Horizontal=�6a   .+�5      �� $��Roboto)�6   ���#3B0F0B$��0,0,15,0q�6>   $��Center=�6P   $��20(�6k   .��6y   Выберите время:6�   �� 5      $��40��6   $��0,0,80,0q�6!     MaterialDesignTextBox#��[ 63   .  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �#c33124)  #MaterialDesignThemes.Wpf.HintAssist   
FontFamily$ Roboto)�6�   $��Center=�6�   $��20(�6�      Hint$ 00��6�   $��Center=�6   � Cursor$ IBeam��6�  ��6;  �� *Time+ �� ValidatesOnDataErrors$
 True��	 �� UpdateSourceTrigger$	 PropertyChanged=�
 � ToolTip
 6�  �� *ErrorCollection[Time]+.��6�   =� ErrorTemplate 5      �� 5      .��5   ,    � 	Resources 5      (  "MaterialDesignThemes.Wpf.SmartHint �� 
TargetType �� Property �� Value��%V   '        �� 5      "  6   .�5      ��  � ��$ Center=�6@   5      5      .��6%   5       �� 5!      $��Roboto)�6   ���#3B0F0B$��0,0,15,0q�6=   $��Center=�6O   $��20(�6j   .��6x   Вид кухни:6�   �� 5"      $��Roboto)�6    �#c33124$ Roboto)�6e   ���#3B0F0B & "MaterialDesignFloatingHintComboBox#��[ 6�   $	��200��6  $��40��6  $��Right=�6'  $ �� IsSynchronizedWithCurrentItem$
 TrueY�6C  ��6�   �� *Types_of_cuisine+ �� SelectedItem 6h  �� *SortByTypes+$	 PropertyChanged=� �� Mode$ TwoWay=�5#      �� 5$   
   $��Hidden=�6   $��30 0 0 0q�6E   $��1��6W   ��6d   �� *Restaurants+ 6�   �� *SelectedRestaurant+��5%      �� 5&      .R�5'      �  � Orientation$ 
Horizontal=�6    5(      ��5*      �� 5+      .R�5,      ]� $	��285��6    $	��335��6,   .+�5-      �� $	��Top=�6    $	��280��66   $	��190��6B   $
��Fill=�6O   ��6^   ��  �� Path$ Restaurant_ImageSource��� 5.      $��0,20,0,0q�6%   .+�5/      ��   MaterialDesignLabel#��[ 6%   $��20(�6R   $��Normal"�6`   $��Roboto)�6t   ���#3B0F0B��6�   �� $
 Name��� 50      #��[ 6$   $��Roboto)�6Q   ���#3B0F0B��6{   �� $ Type_of_cuisine��� 51      $ 
Horizontal=�6)   $��0,0,0,0q�6B   .+�52   "   �� #��[ 6)   $��Roboto)�6V   ���#3B0F0B$��Время работы: ��6�   �� 53   "   #��[ 6)   $��Roboto)�6V   ���#3B0F0B��6�   �� $ 
Start_time��� 54   "   #��[ 6)   $��Roboto)�6V   ���#3B0F0B$��-��6�   �� 55   "   #��[ 6)   $��Roboto)�6V   ���#3B0F0B��6�   �� $ End_time�56      �� 57      $ 
Horizontal=�6)   $��0,0,0,0q�6B   .+�58   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Star=�6:   $��Center=�6F   �� 59   "   #��[ 6(   #��[ 6Y   ��6�   �� $ Rating�5:      5;       J� InputBindings 5<      d� 5=       d� MouseAction$ LeftDoubleClicke�6+    �� Command 6I   �� * DataContext.loadRestaurantPage+ �� RelativeSource ��  �� AncestorType" ��5?      5@      5B      5C      5D      