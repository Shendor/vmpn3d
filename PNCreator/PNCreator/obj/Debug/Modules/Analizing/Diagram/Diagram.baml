   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5   
    	PNCreator)   #PNCreator.Modules.Analizing.Diagram   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
  5   	   \shellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell    	 
  5   
   -   5          � Title$  Анализ данных��5   
   $��612.161��6    $��1121.202��61    � WindowState$ 	Maximized=�6B   
������    WindowStyle#��[  5   %    � Icon$7 1/Resources;component/Content/Images/chart_bar.png��6J    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5        closeCommand��,   &Microsoft.Windows.Shell.SystemCommands   CloseWindowCommand   Command# Z 5	   /   6l     5
   
   -   5        maximizeCommand��   MaximizeWindowCommand# Z 5
   2   6r     5   
   -   5        minimizeCommand��   MinimizeWindowCommand# Z 5   2   6r   .��5      � 
������  � RowDefinitions 5   
   �� 5      $��60 �6   6*   �� 5      -   5        	 ��Name$	 briefHistoryTableRow��5      $
��200* �6:   6J   �� 5      $��30 �6   6*   .+�5   
   �� $��0��6     HeaderBorder#��[ 6   ._�5      �� $��2��6   $��0��6-   $��Center=�6:   
 �� Orientation$
 
Horizontal=�6W   .+�5      �� -   5         ��Name$ 	exportBtn��5      $��2,0,0,0q�6,   {�.    DefaultButtonStyle#��[ 5   /    � ToolTip 5      _� 5      $��Export data table��6"   .��5      �� $3��-/Resources;component/Content/Images/save2.png��6   5      �� 5      -   5        $ printBtn��5      $��7,0,0,0q�6+   #��[ 6R    5      _� 5       $��Print chart��6"   .��5"      �� $3��-/Resources;component/Content/Images/print.png��6   5#      5$      5%      �� 5+   
   $��1��6   $��2��6     ViewportBorder#��[ 63   ._�5,      �  5-      �� 5.      $	��200 �6$   63   �� 5/      6&    � ColumnDefinitions 51      �� 52      $	��200 �6'   �� 53      .+�56      *  $Telerik.Windows.Controls.RadGridView  -   5          Name$ briefHistoryTable��56   &   $��5q�6A   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   
IsReadOnly . $��2��6^   $��0��6r      SelectionMode$ Extended=�57   &    � ContextMenu 58      �� 59      .@�5:      m� -	   5        $��Show details��5:   '   5H      5I      5K      �� 5M      $��1��6   $��0��6*   .+�5N      �� -
   5         ��Name$ 
tableNames��5N      $��30��64   $��5q�6@   6K     5O      -   5        $ historyTable��5O   *   $��5q�6@    . $ Extended=�6]    5P      �� 5Q      .@�5R   "   m� -   5        $��Show data in chart��5R   +   6G   5a      5c      5d      �� 5f      $��0��6   $��2��6,   $��3��6@   $��Bottom=�6K   $��Stretch=�6f   
������ 6�   �� 5g      $��1��6   $��3��6,   $��Stretch=�66   $��Right=�6R   
������ 6�   � 5i      -   5         �Name$ 
chartPanel��5i      $��1��6+   $��1��68   $��5,2,5,2q�5j      
������ 5l      5m      5n      �� 5r   
   $��3��6     FooterBorder#��[ 6   5t      5u      5v      