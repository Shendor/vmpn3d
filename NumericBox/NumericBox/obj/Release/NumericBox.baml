   M S B A M L   `   `   `  ���� E  ANumericBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:NumericBox.ConvertersNumericBox.Converters      NumericBox.NumericBox   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :
converters#clr-namespace:NumericBox.Converters     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $��25��$	��100��$��25��$��50��  � 	Resources  +   %NumericBox.Converters.DoubleConverter   doubleConverter��%   &	          .���  � ColumnDefinitions �� �� $��15 �.+�� -    �Name$ 
PART_Popup�� � AllowsTransparency . $
��Left=�$	��180��$��36��$
��Fade=�.��� .+��� $��1q�
���   �
������� �� CornerRadius$ 2���� $��5q� �� Orientation$ 
Horizontal=�.+��� $��Increment: ��$
��Wrap=�$��14(�$��5,3,5,0q��� -    ��Name$ PART_IncrementTextBox��$��14(�$��80���� -   $ PART_NumericTextBox��$��2��$��0��.�� � ContextMenu �� .@�m� -    m�Name$ PART_MenuItem��$��Options��.��� $��1��	 � RowDefinitions	 �� �� .+��� -   
 ��Name$
 PART_IncreaseButton��$��0��$��	0,0,0,0.2q���� 
���   ����	#FF3c8ce8$	��0.2�� � Points �   �{��@KL �� @KL  �� Stretch$
 Fill=��� -   $
 PART_DecreaseButton��$��1��$��	0,0.2,0,0q���� 
���   ����	#FF3c8ce8$	��0.2�� �   �{���� @KL $
 Fill=�