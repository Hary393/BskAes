   M S B A M L   `   `   `  ����J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  5   	   !    WpfApplication2.OrderWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication2     5   	     � Title$  Złóż zamówienie��5   	   $	��300��6!   $	��300��6.   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	OrderGrid��5         CanUserAddRows . $��	0,10,0,52q�6:      AutoGenerateColumns .  ��5   /   �� *
TestList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Książka��6'      
IsReadOnly .    MinWidth$	 100Q�5   B   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Key+6B     5      $	 100Q�6%    .  $ Ilość��5   ?    6%   �� *Value+6?   5      �� 5   
   -   5        $��Dodaj��5      $
��Left=�6!   $��207,236,0,0q�5      $	��Top=�6&   $��75��6>   $��24��6`   �� 5   
   -   5        $��button1��5      $��Zamów��5      $��25��5      $
��Left=�6   $��
10,235,0,0q�68   $	��Top=�5       $��70��68   �� 5   
   -   5        $��button2��5      $��Anuluj��5      $��25��5      $
��Left=�6   $��
95,235,0,0q�68   $	��Top=�5       $��70��68   5       5!      