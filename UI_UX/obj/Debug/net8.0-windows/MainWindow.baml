   M S B A M L   `   `   `  ����@  <UI_UX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UI_UXUI_UX  5   	   E AViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ViewModels;assembly=ViewModels
ViewModels 5   	       UI_UX.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:UI_UX     5   	   ?local1,clr-namespace:ViewModels;assembly=ViewModels     5   	     � Title$  
MainWindow��5	   	   $	��600��6   $
��1000��6)    � DataContext 5
        ViewModels.FileMonitor  5   
   .��5      � .+�5   
   �� .@�5      �� $��Client Side��6   .��5      � .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��10q�6Y   .+�5      �� $��16(�6(   $
��Bold"�66   $��Center=�6H   Client Information6w   �� 5      -   5        $��Refresh��5   %   $��80��67   $��30��6B   $��10,0,0,0q�6N   6`   5      � 5       � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $��* �6,   �� 5      $��* �6,   .+�5"      �� -   5        $��UserGrid��5"   #   $��2��63   ��6@   �� *
ClientInfo+ �� View 5#      �� 5$   "   -   5        grdTest��.P�5%   &   �� $��ID��65    �� Width$ 50��6e    �� DisplayMemberBinding 6A   �� *ID+6e   �� 5&   &   $��Version��65   $ 80��6o    6F   �� *	Version+6o   �� 5'   &   $��Description��65   $	 100��6w    6J   �� *Description+6w   �� 5(   &   $��	Deprected��65   $ 80��6s    6H   �� *	Deprected+6s   �� 5)   &   $��	CreatedBy��65   $ 80��6s    6H   �� *	CreatedBy+6s   5*   #   5,      5-      �� 50      -   5        $��Upload��50   !   $	��100��62   $��10q�6>   6J   51      52      53      �� 57      $��Server Side��6   .��58      � .+�59      �� .+�5;      �� .+�5<      �� $��16(�6(   $
��Bold"�66   $��10q�6H   Analyzers List6d   �� 5=      -   5         ��Name$ StartServerButton��5=   %   $��Start Server��6@   $	��147��6W   $��30��6c   6o   �� 5>      $��16(�5?       $��Center=�5@       $��Center=�5A       ��5>   (   �� *MessageStatus+5A       5B      &   System.Windows.Controls.DataGrid  5C      -   5        $��AnalyzersList��5C   #      AutoGenerateColumns$ False��68   $	��200��6T   $��10q�6a   	   Columns	 5D      0  *System.Windows.Controls.DataGridTextColumn  5E   "   ,  &System.Windows.Controls.DataGridColumn
   Header$
 Version��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *	Version+  5F   "   $
 Metadata��65    6G   �� *
Metadata+  5G   "   $
 Owner��65    6D   �� *Owner+  5H   "   $
 Date Created��65    6K   �� *DateCreated+5J      �� 5M      $��16(�6$   $
��Bold"�62   $��10q�6D   Add New Analyzer6b   �� 5N      $��Add New Analyzer��6!   $	��150��6<   $��10q�6H   �� 5R      $��16(�6$   $
��Bold"�62   $��10q�6D   Clients Update Status6g     5S      -   5          Name$ ClientsList��5S   #   $ False��68   $	��200��6T   	 5T        5U   "   $
 	Client ID��65    6H   �� *
ClientID+  5V   "   $
 Update Status��65    6L   �� *UpdateStatus+5X      �� 5[      $��16(�6$   $
��Bold"�62   $��10q�6D   
Messages6Z   �� 5\      -   5        $��Messages��5\   $   $,��&No errors or success messages to show.��64   $��10q�6b   5]      5^      5_      5`      5a      5b      