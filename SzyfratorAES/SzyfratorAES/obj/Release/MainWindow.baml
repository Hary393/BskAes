   M S B A M L   `   `   `  ���� G  CSzyfratorAES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SzyfratorAESSzyfratorAES      SzyfratorAES.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:SzyfratorAES       � Title$  Szyfrator Aes��$	��447��$��677.077��.��� .+��� -    ��Name$ comboBox��$
��Left=�$��
157,21,0,0q�$	��Top=�$	��141��.@���  �� 
IsSelected . .��ECB�� .��CBC�� .��CFB�� .��OFB�� -    ��Name$ label��$��Użyj trybu szyfrowania��$
��Left=�$��	22,17,0,0q�$	��Top=�$	��130���� -   $ comboBoxKey��$
��Left=�$��
157,43,0,0q�$	��Top=�$	��141��.@��� .��128�� .��192��  . .��256�� -   $ labelKey��$��Rozmiar klucza��$
��Left=�$��	22,37,0,0q�$	��Top=�$	��130���� -    ��Name$ fileSelectButton��$��Wybierz Plik��$
��Left=�$��	22,68,0,0q�$	��Top=�$��75���� -   $ 
fileLabel1��$��Brak Wybranego Pliku��$
��Left=�$��
22,106,0,0q�$	��Top=�$	��276���� 
���   �$��1q�$
��Left=�$	��300��$��	10,21,0,0q�$	��Top=�$	��288��� $
��Left=�$��26��$��
27,233,0,0q�$	��Top=�$	��259��.+�]� $��0,0,10,0q�.+��� $��Nazwa Pliku Końcowego:��$
��Left=��� -    ��Name$ NazwaWynikowaTxtBox��$��Center=�$��Right=� �� 	MaxLength$ 42��$	��0,4q�$��99��� $
��Left=�$��18��$��
22,352,0,0q�$	��Top=�$	��617���� -   $ 
exitButton��$
��Exit��$
��Left=�$��
22,386,0,0q�$	��Top=�$��75���� -	    ��Name$ usersListView��$
��Left=�$	��152��$��
334,68,0,0q�$	��Top=�$	��132�� �� View �� .P��� �� -
   $ reciversListView��$
��Left=�$	��152��$��
527,68,0,0q�$	��Top=�$	��132�� �� .P��� �� -   $��Marlett)�$��20(�$��4��$��475,109,0,0q�$
��Left=�$��40��$��32��$	��Top=��� -   $��Marlett)�$��20(�$��3��$��475,161,0,0q�$��33��$	��Top=�$
��Left=�$��40���� -   $ label1��$��Lista Użytkowników��$
��Left=�$��
334,21,0,0q�$	��Top=�$	��132���� -   $ label2��$��Lista Odbiorców��$
��Left=�$��
527,21,0,0q�$	��Top=�$	��132��	� -   	 	�Name$	 deszyfrujButton��$��	Deszyfruj��$��162,275,409,114q�	� -   $	 szyfrujButton��$��Szyfruj��$��37,275,541,114q��� -   $ magicButton��$��MAGIC!��$
��Left=�$��564,386,0,0q�$	��Top=�$��75���� -   $ pathSelectButton_Copy��$��Wybierz Gdzie Zapisać��$
��Left=�$��
22,138,0,0q�$	��Top=�$	��148���� -   $ 	pathLabel��$��Brak miejsca docelowego��$
��Left=�$��
22,176,0,0q�$	��Top=�$	��276��