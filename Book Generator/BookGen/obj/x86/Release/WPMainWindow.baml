   M S B A M L   `   `   `  ����   BookGen    BookGen.WPMainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Book Generator��$
��Auto��$
��Auto�� � SizeToContent$ WidthAndHeight=� � Icon$ contents.ico��.��� -    �Name$ ContentGrid��$��
15,0,15,10q�$	��480��$	��640�� � ColumnDefinitions �� $	��110 ��� $��* � � RowDefinitions �� $	��10* ��� $	��80* ��� $	��10* �.+��� -   $��label1��$��0��$��0��$��Steps��$��22(�$��36��$
��Left=�$��0,0,0,2q�$��Bottom=�$��79��$
��Bold"��� $��0��$��0��$��Bottom=�$	��150���� $��1��$��1��$
��Auto��$
��Auto��$
��Auto=�$
��Auto=�$��20,0,0,0q�$	��600��.�� � -   $��	mainFrame��$
��Auto��$
��Auto��  � NavigationUIVisibility$ Hidden=�  � Source$ Pages/Welcome.xaml?�$
��40,0q��� -   $��myPanel��$��1��$��0��$	��Top=�$��0,5,0,0q�.+��� $
��Bold"�$��18(�	Welcome�� $��18(�	Project�� $��18(�
Start page�� $��18(�Pages�� $��18(�
Settings�� $��18(�Build�� $��18(�Finish� -   $ NavigationButtons��$��2��$��1��$
��Auto��$
��Auto��$��0,10,0,0q�.+��� -   $��btnBack��$
��Back��$��23��$��0q�$��75��$
��Left=��� -   $��btnNext��$
��Next��$��23��$��0q�$��75��$��Right=��� -	   $��
saveButton��$��Save project��$��23��$��0,7.5q�$��90��$��Center=�