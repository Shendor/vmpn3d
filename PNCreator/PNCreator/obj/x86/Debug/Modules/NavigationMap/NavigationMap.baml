   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5   
   D @PNCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:PNCreator.Modules.NavigationMapPNCreator.Modules.NavigationMap 5   	   3   -PNCreator.Modules.NavigationMap.NavigationMap   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5   
   =loc-clr-namespace:PNCreator.Modules.NavigationMap     5   	   -   5          � Icon$3  -/Resources;component/Content/Images/frame.png��5   C    � Title$ NavigationMap��5   	   $	��465��6   $	��888��6,   
������  � WindowStyle$ 
ToolWindow=�6Q    � WindowState$ 	Maximized=�5   	      WindowStyle#��[  6S    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5        closeCommand��,   &Microsoft.Windows.Shell.SystemCommands   CloseWindowCommand   Command# Z 5	   /   6l     5
   
   -   5        maximizeCommand��   MaximizeWindowCommand# Z 5
   2   6r     5   
   -   5        minimizeCommand��   MinimizeWindowCommand# Z 5   2   6r   .��5      � .+�5   
   �� -   5        $��scroller��5      $
��Auto=�6'   $
��Auto=�6L   .��5      3  -PNCreator.Modules.NavigationMap.DrawingCanvas  -   5        	  Name$		 map��5      3  -PNCreator.Modules.NavigationMap.DrawingCanvas
   MouseLeftButtonDown$
 map_MouseLeftButtonDown��6k   $��1��6,   $	��320��69   $	��800��6F   
������ 6k   5      5      5      