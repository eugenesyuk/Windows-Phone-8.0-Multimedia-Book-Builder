   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.Pages   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Pages��5      .��5
      � -   5         �Name$ ContentGrid��5
      $��0,0,0,15q�6    .+�5      �� $	��800��6   .+�5      �� $��16(�6   $
��Wrap=�6#   $	��800��67   $
��Left=�6F   �� 6b   $���Now you should add some pages to your book by using the three controls below. The three of them must be used to configure each page you want to add, plus the big rectangle at the bottom of this window.��6f   �� 69  �� 6E  �� 6K  �� 6W  $���The leftmost box is used to add a new page to the book. Click on the "Add" button, and a new window will appear. In this window you can add a name to the page, and a delay in seconds after which the page will automatically turn when its audio has finished playing. The background image for the page can be chosen by clicking on the big rectangle at the bottom of this window. It must be an image of size 480x800 pixels. Then, add some text to the page by using the middle control box. Click on the "Add" button and a new window will appear. Here you can choose an image containing the text (480x800), and the name of the language. Finally, add an audio file to the page by using the last control box. The language name for both the audio file and the text image must match.��6[  6  � 5      -   5        $ BottomControlsGrid��5   
   $��0,10,0,0q�6&    � ColumnDefinitions 5      �� 5      $	��33* �6   �� 5      $	��33* �6   �� 5      $	��33* �6    � RowDefinitions 5      �� 5      $��* �6   .+�5      �� -   5        $��gbPages��5   s   $��0��6   $��0��6"   $��Pages��62   $��90��6A   $
��Left=�6M   $��0q�6h   $	��Top=�6�   $	��260��6�   .��5      � .+�5      �� -   5        $��cbPages��5   U   $��23��6   $
��Left=�6)   $��6,6,0,0q�6D   $	��Top=�6d   $	��170��6|   6�   �� 5      -   5        $��btnPagesSettings��5   g   $��Settings��6   $��23��6.   $��Right=�6:   $��0,6,6,0q�6V   $	��Top=�6   $��60��6�   6�   �� 5      -   5        $��btnPagesAdd��5   b   $	��Add��6   $��23��6)   $
��Left=�65   $��6,35,0,0q�6P   $	��Top=�6u   $��75��6�   6�   �� 5      -   5        $��btnPagesRemove��5   g   $��Remove��6   $��23��6,   $
��Left=�68   $��
101,35,0,0q�6S   $	��Top=�6}   $��75��6�   6�   5      5      �� 5      -   5        $��gbLanguages��5   w   $��0��6   $��1��6"   $��	Languages��62   $��90��6E   $
��Left=�6Q   $��0q�6l   $	��Top=�6�   $	��260��6�   .��5      � .+�5       �� -	   5        $��cbLanguages��5    U   $��23��6   $
��Left=�6)   $��6,6,0,0q�6D   $	��Top=�6h   $	��170��6�   �� 5!      -
   5        $��btnLanguagesSettings��5!   h   $��Settings��6   $��23��6.   $
��Left=�6:   $��	182,6,0,0q�6U   $	��Top=�6�   $��60��6�   6�   �� 5"      -   5        $��btnLanguagesAdd��5"   b   $	��Add��6   $��23��6)   $
��Left=�65   $��6,35,0,0q�6P   $	��Top=�6y   $��75��6�   6�   �� 5#      -   5        $��btnLanguagesRemove��5#   g   $��Remove��6   $��23��6,   $
��Left=�68   $��
101,35,0,0q�6S   $	��Top=�6�   $��75��6�   6�   5$      5%      �� 5&      -   5        $��gbSounds��5&   t   $��0��6   $��2��6"   $��Sounds��62   $��90��6B   $
��Left=�6N   $��0q�6i   $	��Top=�6�   $	��260��6�   .��5'      � .+�5(      �� -   5        $��cbSounds��5(   U   $��23��6   $
��Left=�6)   $��6,6,0,0q�6D   $	��Top=�6e   $	��170��6}   �� 5)      -   5        $��btnSoundsSettings��5)   h   $��Settings��6   $��23��6.   $
��Left=�6:   $��	182,6,0,0q�6U   $	��Top=�6�   $��60��6�   6�   �� 5*      -   5        $��btnSoundsAdd��5*   b   $	��Add��6   $��23��6)   $
��Left=�65   $��6,35,0,0q�6P   $	��Top=�6v   $��75��6�   6�   �� 5+      -   5        $��btnSoundsRemove��5+   g   $��Remove��6   $��23��6,   $
��Left=�68   $��
101,35,0,0q�6S   $	��Top=�6~   $��75��6�   6�   5,      5-      5.      �� 5/      -   5         ��Name$ PagesImagesCanvas��5/      $	��640��6'   $	��480��63   $��0,10,0,0q�6@   $
��Left=�6R   .+�50      �� -   5        $��imgBackground��50   >   $
��Left=�6   $��0q�63   $
��Fill=�6S   $	��Top=�6b   $	��800��6z   $	��480��6�    K� ZIndex$ 1��6�   �� 51      -   5        $��label1��51   �   
�������$&�� Click to change image background��6*   $
��Bold"�6U   $��22(�6g   $	��480��6u   $��0q�6�   $	��800��52      $��Center=�6   $��Center=�6B   6d   53   
   54      55      56      