   M S B A M L   `   `   `  ����   BookGen    BookGen.Pages.StartPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  	StartPage��.��� -    �Name$ ContentGrid��$��0,0,0,15q�.+��� $	��800��.+��� $��16(�$
��Wrap=�$	��800��$
��Left=��� $���In this step, you can choose the first image (cover) for your book. Please click on the big rectangle below to choose a background image for it. Its size must be 800x480 pixels.���� �� �� �� $���Next, you can choose the images for the buttons of the first page's menu. The user will click on the one on the left if she does not want any audio to be played. Otherwise, he can click on the button on the right so the tale will be narrated while reading it.���� �� �� �� $F�@Click on the "Next" button after you have completed these steps.���� -    ��Name$ FirstPageImagesGrid��$	��799��$	��480��$��0,10,0,0q�$
��Left=�.+��� -   $��imgBackground��$	��480��$
��Left=�$��0q�$	��Top=�$	��800�� �� Left$ 0��
 �� Top$ 0�� K� ZIndex$ 3�� � Cursor$
 Hand���� -   $��lblBackground��
�������$��Center=�$��Stretch=�$(��"Click to select a background image��$
��Left=�$��0q�$	��Top=�$ 2��$	��800��$	��480��$��16(�$
 Hand���� -   $��imgReadImage��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$ 5��$	 250��$	 330��$
 Hand���� -   $��imgSoundImage��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$ 5��$	 405��$	 330��$
 Hand���� -   $��lblReadImage��$��Center=�$��Center=�
����ح�$"��Click to select a read image��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$ 4��$	 250��$	 330��$��10.667(�$
 Hand���� -	   $��lblSoundImage��$��Center=�
����ح�$��Center=�$#��Click to select a sound image��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$	 405��$ 4��$	 330��$��10.667(�$
 Hand���� -
    ��Name$ imgReadImag_Clicked��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$ 5��$ 95��$	 330���� -   $ imgSoundImage_Clicked��$	��150��$
��Left=�$��0q�$	��Top=�$	��150��$ 5��$	 560��$	 330��$
 Hand���� -    ��Name$ lblReadImage_Clicked��$��Center=�$��Center=�
����ح�$4��.Click to select a read image
when it's clicked��$	��150��$��0q�$	��150��$ 4��$ 95��$	 330��$��10.667(�$
 Hand���� -   $ lblSoundImage_CLicked��$��Center=�
����ح�$��Center=�$7��1Click to select a sound image 
when it's clicked��$	��150��$��Center=�$��0q�$��Center=�$	��150��$	 560��$ 4��$	 330��$��10.667(�$
 Hand��