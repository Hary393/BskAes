   M S B A M L   `   `   `  ����G  CSzyfratorAES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SzyfratorAESSzyfratorAES  5   	       SzyfratorAES.Login   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SzyfratorAES     5   	     � Title$  Login��5   	   $	��300��6   $	��300��6$    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ local:CustomCommands.Send��5
      6a   .��5      � .+�5   
   � $
��Left=�6   $��26��6*   $��	28,72,0,0q�66   $	��Top=�6I   $	��166��6a   .+�5      ]� $��	0,0,-71,0q�6   .+�5      �� $��Login:��6   $
��Left=�6)   6A   �� 5      -   5         ��Name$ LoginNameTxtBox��5      $��Center=�63   $��Right=�6N    �� 	MaxLength$ 42��6e   $	��0,4q�6t   $	��177��6�   6�   5      5      � 5   
   $
��Left=�6   $��26��6*   $��
30,102,0,0q�66   $	��Top=�6J   $	��238��6b   .+�5      ]� $��2��6   $��2��6(   .+�5      �� $��	Password:��6   $
��Left=�6,   6D   G� 5      -   5         G�Name$ LoginPasswdTxtBox��5      $��Center=�69   $��Right=�6T    G� 	MaxLength$ 42��6k   $	��0,4q�6z   $	��154��6�   6�   5      5      �� 5   
   -   5         ��Name$ Loginbutton��5      $��Login��6&   $
��Left=�66   $��160,165,0,0q�6Q   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $ 
Exitbutton��5      $
��Exit��6%   $
��Left=�64   $��
50,165,0,0q�6O   $	��Top=�6c   $��75��6{   6�   �� 5   
   -   5         ��Name$ RegisterLabel��5      $&�� Don't have an account? Register!��6'   $
��Left=�6R   $��
50,204,0,0q�6m   $	��Top=�6�   �� 5   
   -   5        $ Registerbutton��5      $��Register��6)   $
��Left=�6<   $��105,235,0,0q�6W   $	��Top=�6l   $��75��6�   6�   �� 5   
   -   5        $ 	Infolabel��5      $��SZYFRATOR AES��6#   $
��Left=�6;   $��	68,10,0,0q�6V   $	��Top=�6i   $��20(�6�   5      5       