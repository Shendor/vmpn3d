   M S B A M L   `   `   `  ����H  DAnimatedGraph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:AnimatedGraph;assembly=AnimatedGraphAnimatedGraph  5   	   G CMeshImporter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MeshImporterMeshImporter 5   	       MeshImporter.AddMeshWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Ctree2clr-namespace:AnimatedGraph;assembly=AnimatedGraph     5   	   ,localclr-namespace:MeshImporter     5   	     � AllowsTransparency  .   � Icon$2 ,/Resources;component/Content/Images/plus.png��6M   
������� � WindowState$ 	Maximized=�5       � WindowStyle$
 None=�64    � 
ResizeMode$ NoResize=�6G   .��5      �  � ContextMenu 5	   
   �� 5
      .@�5      m� -   5        $��
Add folder��5      6/   m� 5      -   5        $��Add file��5      6-   m� 5      -   5        $��Close��5      6*   5      .+�5   
   ��    SemiTransparentBorder#��[  6   � 5   
   -   5         �Name$ newNodePopup��5       � AllowsTransparency . $��Mouse=�5      $	��180��6(   $��36��64   $
��Fade=�6@   .��5      ��   ToolTipBorder#��[ 6   $	��180��6<   $��36��6H   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��5q�6    $��Name: ��6+     DefaultLightTextStyle#��[ 69   �� 5      -   5        	 ��Name$	 
meshNameTB��5      $��5q�62   $��85��6=   6H   5      5      5      &    AnimatedGraph.AnimatedTreeViewer  5   
   -   5        
  Name$
 
treeViewer��5   "   5      5      5       