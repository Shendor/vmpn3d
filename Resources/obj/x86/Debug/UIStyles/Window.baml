   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   windowCommandButtons �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius  ButtonBackgroundBrush  BorderBrush �� Triggers	 P� Property
 P� Value �� 
TargetName  HighlightBrush  PressedBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush  WindowStyle*    $Microsoft.Windows.Shell.WindowChrome    GlassFrameThickness    ResizeBorderThickness    CaptionHeight    CornerRadius � RowDefinitions 	 WindowHeaderBorder 
 WindowContentBorder J� Opacity �� RelativeSource �� AncestorType �� Path �� Orientation    IsHitTestVisibleInChrome  LightTextBrush,   &Microsoft.Windows.Shell.SystemCommands   MinimizeWindowCommand   MaximizeWindowCommand   CloseWindowCommand  SmallWindowStyle  SecondaryWindowTemplate  SemiTransparentBorder  ShadowedBorder  HeaderBorder  ViewportBorder  FooterBorder ]� 
Conditions �� Property �� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      PshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell  5        5      �� 5   
   $ Shared.xaml?�6   �� 5   
   $ Border.xaml?�6   %A  &	        7  7  7  7  7  7  7  7  7  7  &	 G    7 	 7 
 7  7  7   7   7   &	     &	 7    7  7  7  7  7  �� 5      " ��6)   .�5   
   ��  �@J�SnapsToDevicePixels . 60   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
    � ��$ 25��6%   �� 5   
    � ��$ 25��6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ Border��6   $ 1��6-   $��1q�6>   8��  8�� ._�5      �� $��2q�6+   $��Center=�66   $��Center=�6S   ��. 6n   5       5      P� 5      	 �@J�IsKeyboardFocused
 . . �5      �� $ Border��6%   8   � ��6P   5      P� 5      	 �@��IsDefaulted
 . . �5      �� $ Border��6%   8   � ��6P   5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ Border��6%   8   � ��6O   5       P� 5!      	 � ��
 . . �5"      �� $ Border��6%   8   � ��6O   �� 5#      $ Border��6%   8   � ��6P   5$      P� 5%      	 � {�
 .  . �5&      �� $ Border��6%   8   � ��6O   �� 5'      $ Border��6%   8   � ��6P   �� 5(      8 	  � ��6;   5)      5+      5-      5.      �� 50      " �6    .�51   
   ��  �@  WindowChrome 52         53      $ -1q�6%   $ 4q�6>   $ 28Q�54   )   $ 0��6<   56      �� 57   
    � �� 58      �� 59      " �6"   .R�5:      �  5;      �� 5<      $��30 �6,   �� 5=      .+�5@      �� $��0��6!   8��  6.   �� 5B      $��1��6!   8�� 6.   �� 5D      $��1,0,1,1q�6!   $��1��6;   8�� $ 1Q�5E   L   $ 0,0,2,2��6X   ��5D   H   ��  �� " �$ 
Background�5G      � 5J      $��0��6   .+�5K      �� $ 
Horizontal=�6)   $��10,7,0,0q�6B   .+�5L   "   �� $��20��6v   $��20��6�   $
��Left=�6�    . ��6(   ��  �� *TemplatedParent+$
 Icon�5M   0   �� 5O   "   $��10,0,0,0q�6y   8�� $��Center=�5P   ]   ��5O   ,   ��  �� *TemplatedParent+$ Title�5P   ]   5R      �� 5T      $ 
Horizontal=�6)   $��Right=�6B   $��0,5,5,0q�6^   $��Center=�6o   .+�5U   "   �� $��MinimizeBtn��6)    . 8�� $��2,0,0,0q�5V   -   #��Z 5W   .   ��5X   -   ��  �� " �$ TemplatedParent�.��5Y   &   �� $3��-/Resources;component/Content/Images/minus.png��6,   $��4q�6c   5Z   #   �� 5\   "    . 8�� $��2,0,0,0q�5]   -   #��Z 5^   .   ��5_   -   ��  �� " �$ TemplatedParent�.��5`   &   �� $7��1/Resources;component/Content/Images/rectangle.png��6,   $��4q�6g   5b   #   �� 5d   "    . 8�� $��2,0,0,0q�5e   -   #��Z 5f   .   ��5g   -   ��  �� " �$ TemplatedParent�.��5h   &   �� $3��-/Resources;component/Content/Images/close.png��6,   $��4q�6c   5i   #   5k      5l      �� 5n      #��z��6+   $��1��6O   $��0q�6\   5o      5q      5s      5t      �� 5v      " �6%   .�5w   
   ��  �@  WindowChrome 5x         5y      $ -1q�6%   $ 0q�6>   $ 28Q�5z   )   $ 0��6<   5|      �� 5}   
    � �� 5~      �� 5      " �6"   .R�5�      � .+�5�      �� #��z��6+   5�      5�      5�      5�      �� 5�      " �66   .R�5�   
   � $��15q�6    5�      �� 5�      $��60 �6    �� 5�      �� 5�      $��30 �6    .+�5�      �� 8��  6   �� 5�      $��3��6   8�� 6&   �� 5�      $��0��6   8�� ._�5�      �� 5�      �� 5�      $��1��6   8�� ._�5�      �� 5�      �� 5�      $ naBorder��6   $	��Top=�6'   $��2��6?   8�� ._�5�      �� $ 
Horizontal=�6   $��0,0,7,2q�66   $��Right=�6G   5�      5�      5�       5�   
   ]� 5�       5�      �� 5�       �@�
ResizeMode$ NoResize=�66   �� 5�       �@�WindowStyle$ Normal=�67   5�      5�      5�      