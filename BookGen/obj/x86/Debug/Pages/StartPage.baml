   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.StartPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  	StartPage��5      .��5	      � -   5         �Name$ ContentGrid��5	      $��0,0,0,15q�6    .+�5
      �� $	��800��6   .+�5      �� $��16(�6   $
��Wrap=�6#   $	��800��67   $
��Left=�6F   �� 6b   $���In this step, you can choose the first image (cover) for your book. Please click on the big rectangle below to choose a background image for it. Its size must be 800x480 pixels.��6f   �� 6!  �� 6-  �� 63  �� 6?  $���Next, you can choose the images for the buttons of the first page's menu. The user will click on the one on the left if she does not want any audio to be played. Otherwise, he can click on the button on the right so the tale will be narrated while reading it.��6C  �� 6P  �� 6\  �� 6b  �� 6n  $F�@Click on the "Next" button after you have completed these steps.��6r  6�  �� 5      -   5         ��Name$ FirstPageImagesGrid��5      $	��640��6)   $	��480��65   $��0,10,0,0q�6B   $
��Left=�6T   .+�5      �� -   5        $��imgBackground��5   K   $	��480��6   $
��Left=�6%   $��0q�6@   $
��Fill=�6`   $	��Top=�6o   $	��800��6�    �� Left$ 0��6�   
 �� Top$ 0��6�    K� ZIndex$ 3��6�   �� 5      -   5        $��lblBackground��5   �   
�������$��Center=�6/   $��Stretch=�6S   $(��"Click to select a background image��6v   $
��Left=�6�   $��0q�6�   $	��Top=�6�   $ 2��6.  $	��800��6?  $	��480��6K  $��16(�6X  �� 5      -   5        $��imgReadImage��5   K   $	��150��6   $
��Left=�6%   $��0q�6@   $
��Fill=�6_   $	��Top=�6n   $	��150��6�   $ 5��6�   $	 250��6�   $	 330��6�   �� 5      -   5        $��imgSoundImage��5   K   $	��150��6   $
��Left=�6%   $��0q�6@   $
��Fill=�6`   $	��Top=�6o   $	��150��6�   $ 5��6�   $	 405��6�   $	 330��6�   �� 5      -   5        $��lblReadImage��5   �   $��Center=�6   $��Center=�6<   
����ح�$"��Click to select a read image��6u   $	��150��6�   $
��Left=�6�   $��0q�6�   $	��Top=�6�   $	��150��6�   $ 4��6>  $	 250��6O  $	 330��6a  $��10.667(�6r  �� 5      -   5        $��lblSoundImage��5   �   $��Center=�6   
����ح�$��Center=�6S   $#��Click to select a sound image��6u   $	��150��6�   $
��Left=�6�   $��0q�6�   $	��Top=�6�   $	��150��6�   $	 405��6A  $ 4��6S  $	 330��6d  $��10.667(�6u  5      5      5      5      