   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.Settings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Settings��5      .��5
      � -   5         �Name$ ContentGrid��5
      $��0,0,0,15q�6    .+�5      �� $	��800��6   .+�5      �� $��16(�6   $
��Wrap=�6#   $	��800��67   $
��Left=�6F   $����In this step you can configure the last aspects of the book. There are three control boxes that you can edit. Use the first one to choose a file name for the resulting XAP file, and to add a title to the book. On the second box you can choose the application icons for the marketplace. Finally, on the third box you can select the icons for the pop-up menu that will appear in each page. The first of these icons is used to play/pause the current audio file, whereas the second one is used to stop it.��6a   �� 5      -   5        $��gbNames��5   _   $��Names��6   $	��100��6#   $��Center=�60   $��0,10,0,0q�6M   $	��Top=�6n   $	��403��6�   .��5      � .+�5      �� -   5        $��
lblXapName��5   W   $��23��6   $
��Left=�6)   $��	14,17,0,0q�6D   $��XAP file name��6i   $	��Top=�6~   �� 5      -   5        $��lblTitle��5   W   $��23��6   $
��Left=�6)   $��	59,0,0,11q�6D   $��Title��6g   $��Bottom=�6t   $��28��6�   �� 5      -   5        $��
txtXapName��5   U   $��23��6   $
��Left=�6'   $��	93,14,0,0q�6B   $	��Top=�6g   $	��281��6   6�   �� 5      -   5        $��txtTitle��5   U   $��23��6   $
��Left=�6'   $��	93,43,0,0q�6B   $	��Top=�6e   $	��281��6}   6�   5      5      �� 5      -   5        $��gbImages��5   k   $��Application icons��6   $	��229��6/   $��Center=�6<   $��0,10,0,0q�6Y   $	��Top=�6{   $	��403��6�   .��5      � .+�5      �� -   5        $��imgLargeIcon��5   T   $	��173��6   $
��Left=�6&   $��	14,15,0,0q�6A   $
��Fill=�6h   $	��Top=�6w   $	��173��6�   6�   �� 5      -	   5        $��imgSmallIcon��5   U   $	��173��6   $
��Left=�6&   $��
201,15,0,0q�6A   $
��Fill=�6i   $	��Top=�6x   $	��173��6�   6�   �� 5      -
   5        $��lblLargeImage��5   �   
�������$j��dThis image is used in the phone start screen. It must have a size of 173 x 173 pixels. Click to add.��64   $	��173��6�   $
��Wrap=�6�   $
��Left=�6�   $��	14,15,0,0q�6�   $	��Top=�6  $	��173��6  6(  �� 5      -   5        $��lblSmallImage��5   �   
�������$l��fThis image is used in the phone application list. It must have a size of 99 x 99 pixels. Click to add.��64   $
��Wrap=�6�   $	��173��6�   $
��Left=�6�   $��
201,15,0,0q�6�   $	��Top=�6  $	��173��6  6+  5      5      �� 5      -   5        $��gbPopupMenu��5   j   $��Popup menu icons��6   $	��191��6.   $��Center=�6;   $��0,10,0,0q�6X   $	��Top=�6}   $	��403��6�   .��5      � .+�5      �� -   5        $��playIcon��5   Q   $	��173��6   $
��Left=�6#   $��	14,15,0,0q�6>   $
��Fill=�6a   $	��Top=�6p   $	��173��6�   �� 5       -   5        $��	pauseIcon��5    U   $	��173��6   $
��Left=�6&   $��
201,15,0,0q�6A   $
��Fill=�6f   $	��Top=�6u   $	��173��6�   �� 5!      -   5        $��lblPlayIcon��5!   �   
�������$��Click to add a play image��64   $	��173��6U   $
��Wrap=�6b   $
��Left=�6v   $��	14,15,0,0q�6�   $	��Top=�6�   $	��173��6�   6�   �� 5"      -   5        $��lblPauseIcon��5"   �   
�������$ ��Click to add a pause image��64   $
��Wrap=�6V   $	��173��6j   $
��Left=�6w   $��
201,15,0,0q�6�   $	��Top=�6�   $	��173��6�   6�   5#      5$      5%      5&      5'      