   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5       	PNCreatorF   @PNCreator.Modules.Simulation.SimulationWindowManipulationToolbar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5      $��Right=�5        �� Orientation$  
Horizontal=�6)   
������ .+�5      �� -   5         ��Name$ minimizeBtn��5      $��5,2,5,2q�6"   $0��*shell:SystemCommands.MinimizeWindowCommand��63    � ToolTip 5   
   _� 5	      $
��Hide��6   .��5   
   �� $3��-/Resources;component/Content/Images/minus.png��6   5      �� 5      -   5        $ upBtn��5      $��5,2,5,2q�6    5   
   _� 5      $��Minimize window��6    � Triggers 5   
   :� 5       :� RoutedEvent$ Button.Click��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ Height�5   *   	 W� To$	 150X�6M    g� Duration$ 0:0:0.5D�6V    �� Target 5   *   �� 	 �� RelativeSource	 �� 
 �� AncestorType"
 �5   V   5      5      5      .��5   
   �� $6��0/Resources;component/Content/Images/arrow_up.png��6   5      �� 5      -   5        $ downBtn��5      $��5,2,5,2q�6    5   
   _� 5       $��Maximize window��6    5"   
   :� 5#      $ Button.Click��6   .[�5$      �� .��5%      �� .��5&      W� $ Height�5'   *   $	 530X�6M   $ 0:0:0.5D�6V    5&   *   �� 	 �� "
 �5'   V   5(      5)      5*      .��5,   
   �� $2��,/Resources;component/Content/Images/dArc.png��6   5-      5.      