   M S B A M L   `   `   `  ���� G  CSzyfratorAES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SzyfratorAESSzyfratorAES      SzyfratorAES.Login   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:SzyfratorAES       � Title$  Login��$	��300��$	��300�� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ local:CustomCommands.Send��.��� .+�� $
��Left=�$��26��$��	28,72,0,0q�$	��Top=�$	��166��.+�]� $��	0,0,-71,0q�.+��� $��Login:��$
��Left=��� -    ��Name$ LoginNameTxtBox��$��Center=�$��Right=� �� 	MaxLength$ 42��$	��0,4q�$	��177��� $
��Left=�$��26��$��
30,102,0,0q�$	��Top=�$	��238��.+�]� $��2��$��2��.+��� $��	Password:��$
��Left=�G� -    G�Name$ LoginPasswdTxtBox��$��Center=�$��Right=� G� 	MaxLength$ 42��$	��0,4q�$	��154���� -    ��Name$ Loginbutton��$��Login��$
��Left=�$��160,165,0,0q�$	��Top=�$��75���� -   $ 
Exitbutton��$
��Exit��$
��Left=�$��
50,165,0,0q�$	��Top=�$��75���� -    ��Name$ RegisterLabel��$&�� Don't have an account? Register!��$
��Left=�$��
50,204,0,0q�$	��Top=��� -   $ Registerbutton��$��Register��$
��Left=�$��105,235,0,0q�$	��Top=�$��75���� -   $ 	Infolabel��$��SZYFRATOR AES��$
��Left=�$��	68,10,0,0q�$	��Top=�$��20(�