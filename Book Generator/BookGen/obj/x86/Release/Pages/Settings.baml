   M S B A M L   `   `   `  ����   BookGen    BookGen.Pages.Settings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     N� Title$  Settings��$
��1010��.��� -    �Name$ ContentGrid��.+��� $	��800��$
��1000��$	��Top=�.+��� $��16(�$
��Wrap=�$	��800��$
��Left=�$����In this step you can configure the last aspects of the book. There are three control boxes that you can edit. Use the first one to choose a file name for the resulting XAP file, and to add a title to the book. On the second box you can choose the application icons for the marketplace. On the third box you can select the icons for the pop-up menu that will appear in each page. The first of these icons is used to play/pause the current audio file, whereas the second one is used to stop it. Finally , you must select image , showed when the application is loading.���� -   $��gbNames��$��Names��$	��100��$��Center=�$��0,10,0,0q�$	��Top=�$	��403��.��� .+��� -   $��
lblXapName��$��23��$
��Left=�$��	14,17,0,0q�$��XAP file name��$	��Top=��� -   $��lblTitle��$��23��$
��Left=�$��	59,0,0,11q�$��Title��$��Bottom=�$��28���� -   $��
txtXapName��$��23��$
��Left=�$��	93,14,0,0q�$	��Top=�$	��281���� -   $��txtTitle��$��23��$
��Left=�$��	93,43,0,0q�$	��Top=�$	��281���� -   $��gbImages��$��Application icons��$	��229��$��Center=�$��170,10,170,0q�$	��Top=�$	��402��.��� .+��� -	   $��imgLargeIcon��$	��173��$
��Left=�$��	14,15,0,0q�$
��Fill=�$	��Top=�$	��173�� � Cursor$
 Hand�� J� 	AllowDrop . |�.  � ToolTip$ Application icon 173x173���� -
   $��imgSmallIcon��$	��173��$
��Left=�$��
201,15,0,0q�$
��Fill=�$	��Top=�$	��173��$
 Hand��$ Application icon 99x99�� . |�. �� -   $��lblLargeImage��
�������$j��dThis image is used in the phone start screen. It must have a size of 173 x 173 pixels. Click to add.��$	��173��$
��Wrap=�$
��Left=�$��	14,15,0,0q�$	��Top=�$	��173��$
 Hand�� �� TextAlignment$ Center=� �� 
LineHeight$ 10���� -   $��lblSmallImage��
�������$l��fThis image is used in the phone application list. It must have a size of 99 x 99 pixels. Click to add.��$
��Wrap=�$	��173��$
��Left=�$��
201,15,0,0q�$	��Top=�$	��173��$
 Hand��$ Center=�$ 10�� . |�. $ Application icon 99x99���� -   $��gbPopupMenu��$��Popup menu icons��$	��229��$��Center=�$��170,10,170,0q�$	��Top=�$	��402��.��� $��0,0,0,-1q�.+��� -   $��playIcon��$	��173��$
��Left=�$��	14,15,0,0q�$	��Top=�$	��173��$
 Hand��$
��Fill=�$ 
Play image�� . |�. �� -   $��	pauseIcon��$	��173��$
��Left=�$��
201,15,0,0q�$	��Top=�$	��173��$
��Fill=�$
 Hand��$ Pause image�� . |�. �� -   $��lblPlayIcon��
�������$ Center=�$��Click to add a play image��$	��173��$
��Wrap=�$
��Left=�$��14,15,0,-19q�$	��Top=�$	��173��$
 Hand��$ Play image �� . |�. �� -   $��lblPauseIcon��
�������$ Center=�$ ��Click to add a pause image��$
��Wrap=�$	��173��$
��Left=�$��201,15,0,-19q�$	��Top=�$	��173��$
 Hand��$ Pause image�� . |�. �� -    ��Name$ gbPopupMenu_Copy��$��Application load icon��$	��260��$��Center=�$��170,10,170,0q�$	��Top=�$	��402��.��� .+��� -    ��Name$ loadicon��$	��216��$
��Left=�$��	14,15,0,0q�$	��Top=�$	��360��$
��Fill=�$
 Hand��$$ Application load image 480x800�� . |�. �� -   	 ��Name$	 lblLoadIcon��
�������$ Center=�$g��aThis image is used when the application is loading. It must have a size of 480x800. Click to add.��$
��Wrap=�$
��Left=�$��
14,15,0,10q�$	��360��$
 Hand��$$ Application load image 480x800�� . |�. 