   M S B A M L   `   `   `  ����  BookGen    BookGen.Pages.IBPages   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $��LeftToRight=�5      $	��810��5      $	��672��6     N� WindowWidth$	  640Q�5	       N� WindowHeight$	 480Q�6   .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   �� $��68��6   $��8,8,8,0q�6    $
��Wrap=�61   $	��Top=�6E   $��16(�6]   �� 6l    � Language$ pl-pl�6p   $[�UIn this step can set images for every page (left and right part) and add sound files.��6�   �� 6�   �� 6�   $ pl-pl�6�   $X�RIf you added sound you can check file name if you move curson on the note's image.��6  �� 6_  �� 6k  $ pl-pl�6o  $g�aOne page part must be an image of size 512x768 pixels as .png file. Sound must be in .mp3 format.��6�  6�  �� 5   
   $��Pages��6   $
��Left=�6"   $��81.797��6=   $��	73,95,0,0q�6M   $	��Top=�6`   $	��320��6x   .��5      � .+�5      �� -   5         ��Name$ cmbSelectedPage��5      $��23��64   $	��Top=�6@   $��0,0,5,0q�6X   6i   �� 5      -   5         ��Name$ btnRemovePage��5      $��Remove Pages��60   $��23��6G   $��
206,29,0,0q�6S   $	��Top=�6g   $
��Left=�6   $��97��6�   6�   �� 5      -   5        $ 
btnAddPage��5      $��	Add Pages��6-   $��23��6A   $��
105,29,0,0q�6M   $	��Top=�6a   $
��Left=�6y   $��97��6�   6�   �� 5      -   5        $ btnSavePage��5      $��Save changes��6.   $��23��6E   $��4,29,0,0q�6Q   $	��Top=�6c   $
��Left=�6{   $��97��6�   6�   5      5      �� 5   
   $��Preview��6   $��Center=�6$   $��81.797��6A   $��
160,94,0,0q�6Q   $	��Top=�6e   $	��150��6}   .��5      � .+�5      �� -   5        $��	isPreview��5   n   $��Use in preview��6   $��16��64   $��Center=�6@   $��0,0,0,0q�6]   $��Center=�6    � ToolTip$- 'Check if this page should be in preview��6�   6�   5      5      �� 5   
   $��Sound��6   $��Right=�6"   $��81.797��6>   $��	0,94,77,0q�6N   $	��Top=�6a   $	��168��6y   .��5      � .+�5       �� -   5        $ btnRemoveSound��5       $��Remove Sound��61   $��23��6H   $��	0,31,55,0q�6T   $	��Top=�6g   $��Right=�6   $��97��6�   6�   �� 5!      -   5        $ btnAddSound��5!      $��Add Sound...��6.   $��23��6E   $��0,5,55,0q�6Q   $	��Top=�6c   $��Right=�6{   $��97��6�   6�   �� 5"      -	   5        $��imgSoundStatus��5"   v   $��Right=�6   $��30��64   $��	0,15,12,0q�6@   $	��Top=�6S   $��30��6k   $/��)/BookGen;component/Resources/soundoff.png��6�   $ No file��6�   5#      5$      �� 5&   
   $��73,181,77,11q�6   $	��660��6'   .+�5'      �� -
   5         ��Name$ imgLeft��5'      $	��480��6%   $	��320��62    �� Left$ 0��6?   $
��Fill=�6�   �� 5(      -   5        $ imgRight��5(      $	��480��6&   $	��320��63   $	 340��6?   $
��Fill=�6�   �� 5)      -   5        $��lblLeft��5)   �   $%��Click to change left page image��6   $	��480��6>   $	��320��6K   
�������$ 0��6�   6�   �� 5*      -   5        $��lblRight��5*   �   $&�� Click to change right page image��6   $	��480��6?   $	��320��6L   
�������$	 340��6�   6�   5+      5,      5-      