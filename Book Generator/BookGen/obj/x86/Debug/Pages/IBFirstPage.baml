   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.IBFirstPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  IBFirstPage��5      |�. .��5
      � -   5         �Name$ ContentGrid��5
      $��0,0,0,15q�6    |�.  .+�5   
   �� $	��800��6   |�.  .+�5      �� -   5         ��Name$
 text��5      $��16(�6&   $
��Wrap=�64   $	��800��6H   $
��Left=�6W   �� 6s   $���In this step, you can choose first page image for your book. Please click the open button to choose a background image for it. Its size must be 512x768 pixels.��6w   �� 6   �� 6,  �� 62  �� 6>  $F�@Click on the "Next" button after you have completed these steps.��6B  6�  � 5       � ColumnDefinitions 5      �� 5      $
��auto �6'   66   �� 5      $��* �6'   63   .+�5      � $��0��6   .+�5      � -   5        $ grid1��5   W   $
��auto��6   $
��Left=�6)   $��	12,12,0,0q�6D   $	��Top=�6f   $	��512��6~   .+�5      �� -   5         ��Name$ FirstPageImagesGrid��5      $	��512��6:   $	��768��6F   $��	15,10,0,0q�6S   $
��Left=�6f   .+�5      �� -   5        $��imgBackground��5   _   $	��768��6    $	��512��6-   $
��Left=�69   $��0q�6T   $
��Fill=�6t   $	��Top=�6�    �� Left$ 0��6�   
 �� Top$ 0��6�    K� ZIndex$ 0��6�   5      5      5      � 5      $��1��6   |�.  .+�5      �� -   5        $��button1��5   g   $
��Open��6   $��23��6,   $��Center=�68   $��0,33,0,0q�6U   $	��Top=�6v   $��75��6�   6�   �� 5      -   5        $��button2��5   h   $
��Save��6   $��23��6,   $��Center=�68   $��	0,104,0,0q�6U   $	��Top=�6w   $��75��6�   6�   �� 5      -	   5        $��button3��5   h   $
��Font��6   $��23��6,   $��Center=�68   $��	0,218,0,0q�6U   $	��Top=�6w   $��75��6�   6�   �� 5      -
   5        $��button4��5   i   $��Color��6   $��23��6-   $��Center=�69   $��	0,164,0,0q�6V   $	��Top=�6x   $��75��6�   6�   5       5"      5#      5$      5%      