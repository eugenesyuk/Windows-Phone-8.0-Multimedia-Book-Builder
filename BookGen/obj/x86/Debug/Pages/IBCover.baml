   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.IBCover   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  IBCover��5       � 	Resources 5	         mouseOverColor �� Color  myStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property	 P� Value��%I  &	        &	 !     7   �� 5
   
   $	 Red��5      �� 5   
   " ��6    .�5      ��  � ��$ Center=�64   �� 5       � �� 5      �� 5      " ��6&    5      P� 5       �@J�IsMouseOver	 . . �5   "   �� 8    � ��5      5      .R�5      �� 5      5      5      5      .��5      � -   5        
 �Name$
 ContentGrid��5      $��0,0,0,15q�6    .+�5   
   �� $	��800��6    J� OpacityMask �	#FF525252.+�5       �� $��16(�6   $
��Wrap=�6&   $	��800��6:   $
��Left=�6I   $��10,10,10,10q�6d   $	��782��6y   �� 6�   $���In this step, you can produce cover for your book. Please click on the big rectangle below to choose a background image for it. Its size must be 1024x768 pixels. Also you must input additional parameters of your cover , such as width and length what would you wish. Please enter author and title for cover too.��6�   �� 6�  �� 6�  �� 6�  �� 6�  $F�@Click on the "Next" button after you have completed these steps.��6�  6A  �� 5!      -   5         ��Name$ FirstPageImagesGrid��5!      $	��800��62   $	��600��6>   $��	0,115,0,0q�6K   $��Center=�6^   $��Center=�6{   .+�5"      �� -   5        $��imgBackground��5"   >   $
��Left=�6   $��0q�63   $��Uniform=�6S   $	��Top=�6f    �� Left$ 0��6�   
 �� Top$ 0��6�    K� ZIndex$ 3��6�    �	#FF727272 � DataContext 6�   �� 64  �� 5#      -   5        $��lblBackground��5#   �   #��[ 6   
�������$��Center=�6L   $��Center=�6p   $#��Click to select a cover image��6�   $��Center=�6�   $��0q�6�   $��Center=�6�   $ 2��6J  $��16(�6[  $	 301��6i  $ 12��6{  
���   �$	��100��6�  $	��201��6�  5$      5%      �� 5&   
   $��0,121,0,546q�6   .+�5'      �� $ 30��6   $ 10��6%   $��Author for cover:��65   $��25��6Q   $	��112��6]   #��[ 6i   $��Center=�6�   �� 5(      -   5         ��Name$ txtIBAuthorCover��5(      $	 148��60   $ 10��6B   $��25��6R    �� TextWrapping$
 Wrap=�6^   $	��286��6r    �� MaxLines$ 1��6~    �� 	MaxLength$ 25��6�   $��Stretch=�6�   $��Stretch=�6�   �� 5)      $��Title for cover:��6   $��25��6/   $	��112��6;   $ 30��6G   $ 40��6X   #��[ 6h   �� 5*      -   5        $ txtIBTitleCover��5*      $��25��6/   $
 Wrap=�6;   $	��286��6O   $	 148��6[   $ 40��6m   6}   �� 5+      -   5        $��
FontButton��5+      $��	0,10,25,0q�6'   $��80��6:   $��23��6E   $
��Font��6Q   $	 148��6`   $ 70��6r   6�   �� 5,      -	   5        $��image1��5,   D   $	 647��6   $	 -15��6&   $	��125��67   $
��Fill=�6R   $	��129��6a   $+��%/BookGen;component/Resources/size.png��6m   �� 5-      $	 456��6   $ 10��6&   $��X px:��66   $��25��6F   $��47��6R   #��[ 6]   �� 5.      $	 456��6   $ 40��6&   $��Y px:��66   $��25��6F   $��47��6R   #��[ 6]   �� 5/      $	 456��6   $ 70��6&   $��Z px:��66   $��25��6F   $��47��6R   #��[ 6]   �� 50      -
   5        $
��Xbox��50   D   $	 509��6   $ 10��6(   $��25��68   $	��120��6P   6�   �� 51      -   5        $
��Ybox��51   D   $	 509��6   $ 40��6(   $��25��68   $	��120��6P   6w   �� 52      -   5        $
��Zbox��52   D   $	 509��6   $ 70��6(   $��25��68   $	��120��6P   6�   53      54      55      