   M S B A M L   `   `   `  ����   BookGen    BookGen.Pages.Pages   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  Pages��.��� -    �Name$ ContentGrid��$��0,0,0,15q�.+��� $	��800��.+��� $��16(�$
��Wrap=�$	��800��$
��Left=��� $���Now you should add some pages to your book by using the three controls below. The three of them must be used to configure each page you want to add, plus the big rectangle at the bottom of this window.���� �� �� �� $���The leftmost box is used to add a new page to the book. Click on the "Add" button, and a new window will appear. In this window you can add a name to the page, and a delay in seconds after which the page will automatically turn when its audio has finished playing. The background image for the page can be chosen by clicking on the big rectangle at the bottom of this window. It must be an image of size 480x800 pixels. Then, add some text to the page by using the middle control box. Click on the "Add" button and a new window will appear. Here you can choose an image containing the text (480x800), and the name of the language. Finally, add an audio file to the page by using the last control box. The language name for both the audio file and the text image must match.��� -   $ BottomControlsGrid��$��0,10,0,0q� � ColumnDefinitions �� $	��33* ��� $	��33* ��� $	��33* � � RowDefinitions �� $��* �.+��� -   $��gbPages��$��0��$��0��$��Pages��$��90��$
��Left=�$��0q�$	��Top=�$	��260��.��� .+��� -   $��cbPages��$��23��$
��Left=�$��6,6,0,0q�$	��Top=�$	��170���� -   $��btnPagesSettings��$��Settings��$��23��$��Right=�$��0,6,6,0q�$	��Top=�$��60���� -   $��btnPagesAdd��$	��Add��$��23��$
��Left=�$��6,35,0,0q�$	��Top=�$��75���� -   $��btnPagesRemove��$��Remove��$��23��$
��Left=�$��
101,35,0,0q�$	��Top=�$��75���� -	   $��gbLanguages��$��0��$��1��$��	Languages��$��90��$
��Left=�$��0q�$	��Top=�$	��260��.��� .+��� -
   $��cbLanguages��$��23��$
��Left=�$��6,6,0,0q�$	��Top=�$	��170���� -   $��btnLanguagesSettings��$��Settings��$��23��$
��Left=�$��	182,6,0,0q�$	��Top=�$��60���� -   $��btnLanguagesAdd��$	��Add��$��23��$
��Left=�$��6,35,0,0q�$	��Top=�$��75���� -   $��btnLanguagesRemove��$��Remove��$��23��$
��Left=�$��
101,35,0,0q�$	��Top=�$��75���� -   $��gbSounds��$��0��$��2��$��Sounds��$��90��$
��Left=�$��0q�$	��Top=�$	��260��.��� .+��� -   $��cbSounds��$��23��$
��Left=�$��6,6,0,0q�$	��Top=�$	��170���� -   $��btnSoundsSettings��$��Settings��$��23��$
��Left=�$��	182,6,0,0q�$	��Top=�$��60���� -   $��btnSoundsAdd��$	��Add��$��23��$
��Left=�$��6,35,0,0q�$	��Top=�$��75���� -   $��btnSoundsRemove��$��Remove��$��23��$
��Left=�$��
101,35,0,0q�$	��Top=�$��75���� -    ��Name$ PagesImagesCanvas��$	��640��$	��480��$��0,10,0,0q�$
��Left=�.+��� -   $��imgBackground��$
��Left=�$��0q�$
��Fill=�$	��Top=�$	��800��$	��480�� K� ZIndex$ 1���� -   $��label1��
�������$&�� Click to change image background��$
��Bold"�$��22(�$	��480��$��0q�$	��800��$��Center=�$��Center=�