   M S B A M L   `   `   `  ����b  ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35bFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5      D @PNCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PNCreator.CommandsPNCreator.Commands 5      *   $PNCreator.Modules.Main.Menu.MainMenu   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5      5commands clr-namespace:PNCreator.Commands     5      -   5        
������ .@�5      m� $
��File��6   
������ .@�5	   
   m� -   5          m�Name$  newMI��5	      $��	New model��6"    m� Command$	 New��65   m� 5
   
   -   5        $  openMI��5
      $
��Open��6#   $
 Open��61   m� 5   
   -   5        $  openRecentMI��5      $��Open recent��6)   $y�	Collapsed=�6>   m� 5   
   -   5        $  saveMI��5      $
��Save��6#   $
 Save��61   m� 5   
   -   5        $  saveAsMI��5      $��Save as��6%   $ SaveAs��66   m� 5   
   $��Simulation Data��6   $ SaveAs��6,   .@�5      m� -   5        $  importSimulationHistoryMI��5      $��Import��6:   m� 5      -   5        $  exportSimulationHistoryMI��5      $��Export��6:   5      m� 5   
   -	   5        $  exitMI��5      $
��Exit��6#   ,   &Microsoft.Windows.Shell.SystemCommands   CloseWindowCommand# Z 61   5      m� 5      $
��Edit��6   .@�5   
   m� -
   5        $  findMI��5      $��Find object��6#   $
 Find��68   m� 5   
   -   5        $  modelConfigMI��5      $��Model configuration��6*   m� 5   
   -   5        $  clusterConfigMI��5      $��Cluster configuration��6,   5      m� 5      $
��View��6   .@�5   
   m� -   5        $  positioniserMI��5       m� IsCheckable . $��Object positioniser��6>    m� 	IsChecked . 6[   m� 5   
   -   5        $  showMembranesMI��5      $��Show membranes��6,    .  . 6W   m� 5    
   -   5        $  propertiesMI��5       $��
Properties��6)   $ 
Properties��6=   m� 5!   
   -   5        $  modelInfoMI��5!      $��Model Information��6(   m� 5#   
   $��Camera��6   .@�5$      m� $��Perspective��6   $* $commands:MPNCommands.PerspectiveView��6,   m� 5%      $	��Top��6   $$ commands:MPNCommands.FrontView��6$   5&      5*      m� 5,      $��Analize��6   .@�5-   
   m� -   5        $  diagrammesMI��5-      $��
Diagrammes��6)   m� 5.   
   -   5        $  chartsMI��5.      $��Charts��6%   5/      m� 51      $��Objects��6   .@�52   
   m� -   5        $  coveObjectsMI��52      $ ��Cover objects by membranes��6*   53      m� 5<      $��	Reference��6   .@�5=   
   m� -   5        $  helpMI��5=      $
��Help��6#   m� 5>   
   -   5        $  aboutMI��5>      $��About��6$   5?      5@      