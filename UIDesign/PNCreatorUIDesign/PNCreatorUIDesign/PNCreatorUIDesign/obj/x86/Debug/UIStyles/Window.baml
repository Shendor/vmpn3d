   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   windowCommandButtons �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius  ConvexHorizontalBrush  BorderBrush �� Triggers	 P� Property
 P� Value �� 
TargetName  SelectedHorizontalBrush  PressedBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush  WindowStyle*    $Microsoft.Windows.Shell.WindowChrome    GlassFrameThickness    ResizeBorderThickness    CaptionHeight    CornerRadius � RowDefinitions 	 WindowHeaderBackground 
 WindowBackgroundBrush J� Opacity �� Orientation    IsHitTestVisibleInChrome �� RelativeSource �� Path,   &Microsoft.Windows.Shell.SystemCommands   MinimizeWindowCommand �� AncestorType  HighlightBrush   MaximizeWindowCommand   CloseWindowCommand � Points �� Stretch �� Angle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      PshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell  5        5      �� 5   
   $ Shared.xaml?�6   %�  &	        7  7  7  7  7  7  7  7  7  7  &	 J    7 	 7  7  7 
 7  7  7   7  7  7   7  7  7   7  7  �� 5
      " ��6)   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � ��$ 35��6%   �� 5   
    � ��$ 22��6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ Border��6   $ 0,0,2,2��6-   $��1q�6D   8��  8�� ._�5      �� $��2q�6+   $��Center=�66   $	��Top=�6S   ��. 6k   5       5      P� 5      	 �@J�IsKeyboardFocused
 . . �5      �� $ Border��6%   8   � ��6P   5      P� 5      	 �@��IsDefaulted
 . . �5      �� $ Border��6%   8   � ��6P   5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ Border��6%   8   � ��6O   5      P� 5      	 � ��
 . . �5       �� $ Border��6%   8   � ��6O   �� 5!      $ Border��6%   8   � ��6P   5"      P� 5#      	 � {�
 .  . �5$      �� $ Border��6%   8   � ��6O   �� 5%      $ Border��6%   8   � ��6P   �� 5&      8 	  � ��6;   5'      5)      5+      5,      �� 5/      " �6    .�50   
   ��  � ��
 ���� 6'   �� 51   
    �@  WindowChrome 52         53      $ -1q�6%   $ 4q�6>   $ 28Q�54   )   $ 0��6<   56      �� 57   
    � �� 58      �� 59      " �6"   .R�5:      �  5;      �� 5<      $��30 �6,   �� 5=      .+�5@      �� $��1q�6!   $��0��65   $ 2,2,0,0��6C   8��  8�� 6�   �� 5B      $��1,0,1,1q�6!   $��1��6;   
������ 8�� $ 0,0,2,2��6�   �� 5D      $��1,0,1,1q�6!   $��1��6;   8�� 8�� $ 0Q�5E   !   $ 0,0,2,2��6-   5G      � 5J      $��0��6   .+�5K      �� $ 
Horizontal=�6)   $��10,7,0,0q�6B   .+�5L   "   �� $��20��6v   $��20��6�   $
��Left=�6�    . ��6(   ��  �� *TemplatedParent+$
 Icon�5M   0   �� 5O   "   $��10,0,0,0q�6y   8�� $��Center=�5P   Z   ��5O   ,   ��  �� *TemplatedParent+$ Title�5P   Z   5R      �� 5T      $ 
Horizontal=�6)   $��Right=�6B   $��0,0,10,0q�6^   $	��Top=�6p   .+�5U   "   �� $��MinimizeBtn��6)    . 8�� $��2,0,0,0q�5V   -   #��Z 5W   .   ��5X   -   ��  �� " �$ TemplatedParent���5Y   &   �� 5Z   *   $��15��64   $��5��6?   $��0,12,0,0q�6J   8�� $ 1Q�6�   8�� 5[   0   5]   #   �� 5_   "    . 8��	 $��2,0,0,0q�5`   -   #��Z 5a   .   ��5b   -   ��  �� " �$ TemplatedParent�.��5c   &   �� $��14��60   $��12��6;   $��	0,5.2,0,0q�6G   8��
 $ 1Q�6�   8�� 5d   0   5f   #   �� 5h   "    . $��35��6\   8�� $��2,0,0,0q�5i   -   #��Z 5j   .   ��5k   -   ��  �� " �$ TemplatedParent���5l   &   � 5m   *   q �   ��- �� �� �� �� ��- ��- �� @KL �� @KL ��- ��- ��- ��- @KL �� @KL �� ��- $��
14,0.4,0,0q�6k   $��24��6   $��15��6�   $��1��5n   2   8�� 8�� $ Uniform=�6�   z�5o   .   �� 5p   2   $ 45Q�6B   5r   +   5t   #   5v      5w      �� 5y      #��z��6+   $��1��6O   $��5,0,5,10q�6\   5z      5|      5~      5      5�      