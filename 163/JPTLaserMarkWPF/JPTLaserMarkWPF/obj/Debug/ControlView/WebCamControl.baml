   M S B A M L   `   `   `  ����a  ]PresentationFramework.Aero, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35gJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroMicrosoft.Windows.Themes  5       JPTLaserMarkWPF/   )JPTLaserMarkWPF.ControlView.WebCamControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ]ThemesJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero     5      $	��300��5   8   $	��750��6E     � 	Resources  5	         ComboBoxFocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color � Offset  ButtonNormalBorder	 �� Color  DownArrowGeometry   ComboBoxReadonlyToggleButton
 �� 
TargetType �� 
TargetType+   %Microsoft.Windows.Themes.ButtonChrome  Name   BorderBrush   
Background J� IsMouseOver   RenderMouseOver   RenderPressed F�Name �� Triggers P� Property P� Value �� 
TargetName  TextBoxBorder � MappingMode  ComboBoxEditableTextBox ��Name  ComboBoxToggleButton   RoundCorners  ComboBoxEditableTemplate �Name � ColumnDefinitions �Name � AllowsTransparency �� RelativeSource5   /Microsoft.Windows.Themes.SystemDropShadowChrome  Name    Color! �� MaxDropDownHeight" �� ElementName# ��Name-  'System.Windows.Input.KeyboardNavigation$   DirectionalNavigation,   &Microsoft.Windows.Themes.ListBoxChrome%  Name&   BorderBrush'   BorderThickness(   
Background)   RenderMouseOver* J� IsKeyboardFocusWithin+   RenderFocused, ��Name- �� 
IsReadOnly. d� 	IsChecked/ �� Mode0 P� 
SourceName 	 ComboBoxStyle11 �� SelectionBoxItemTemplate2 �� SelectionBoxItem#3 �� SelectionBoxItemStringFormat4 �� ContentStringFormat5 J� IsHitTestVisible6 �� Triggers��%�  &	        &	 �     &	 �    &	 �    &	 �    7  &	     &	 �    &	 �    7  &	 �
    7  7  &		 <    7   7  7  7  7  �� 5
   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��4,4,21,4q�6$    . 
���   �$��1��6`   $	 1 2R�6t   5      5      5      �� 5   
   $	 0,1&�6=   $	 0,0&�6L   .��5      � $ #F3F3F3��6   $ 0Q�6+   � 5      $ #EBEBEB��6   $	 0.5Q�6+   � 5      $ #DDDDDD��6   $	 0.5Q�6+   � 5      $ #CDCDCD��6   $ 1Q�6+   5      �� 5   
   $	 	#FF707070��65   �5   
   M 0 0 L 3.5 4 L 7 0 Z�6D   �� 5   
   "
 d�65   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � �� .  6*   �� 5       � |� .  6*   �� 5       �@��	ClickMode$ Press=�6*   �� 5        � �� 5!      �� 5"      " d�6&   .R�5#        $ Chrome��6.   # z��6>   # z��6j   # z 6�   # z��6�    . ._�5$      � $��Right=�6#   #��� r�6?   .+�5%   "   F� $ Arrow��6'   8��  
���   �$��Center=�6m   $��3,1,0,0q�6�   $��Center=�6�   5&      5'       5(      P� 5)       �@d�	IsChecked$
 trueY�6;   . �5*   "   �� $ Chrome��6B    �@ RenderPressed . 6V   5+      P� 5,       � {� .  . �5-   "   �� $ Arrow��69    � �� �#AFAFAF6L   5.      50      52      53      �� 54   
   $
 0,20&�64   $ Absolute=�6D   $	 0,0&�6[   .��55      � $ #ABADB3��6   $
 0.05Q�6+   � 56      $ #E2E3EA��6   $
 0.07Q�6+   � 57      $ #E3E9EF��6   $ 1Q�6+   58      �� 59   
   "
 ��60   .�5:      ��  �@�OverridesDefaultStyle . 66   �� 5;       �@J�	AllowDrop . 6*   �� 5<       � ��$ 0��6)   �� 5=       � ��$ 0��6*   �� 5>       �@�FocusVisualStyle 61   Z� �� 5?       � �� 5@      �� 5A      " ��6&   .R�5B      �� $ PART_ContentHost��6'   
������ |�.  $��Hidden=�6l   $��Hidden=�6�   5C      5E      5F      �� 5G   
   "
 d�6-   .�5H      ��  �@�OverridesDefaultStyle . 66   �� 5I       � �� .  6*   �� 5J       � |� .  6*   �� 5K       �@��	ClickMode$ Press=�6*   �� 5L       � �� 5M      �� 5N      " d�6&   .R�5O        $ Chrome��6.   # z��6>   # z��6j   # z 6�   # z��6�    .   . #��� r�6   ._�5P      F� $ Arrow��6#   8��  
���   �$��Center=�6i   $��0,1,0,0q�6�   $��Center=�6�   5Q       5R      P� 5S       �@d�	IsChecked$
 trueY�6;   . �5T   "   �� $ Chrome��6B    �@ RenderPressed . 6V   5U      P� 5V       � {� .  . �5W   "   �� $ Arrow��69    � �� �#AFAFAF6L   5X      5Z      5\      5]      �� 5^   
   " ��6;   .R�5_      � $ 	Placement��6    .  5`      �� 5a      $��* �6'   �� 5b      $
��Auto �6'   .+�5d      � $ 
PART_Popup��6    . $��2��6F   #��� .�6�   $��Bottom=�6  ��6Z   �� *IsDropDownOpen+ �� *TemplatedParent+.��5e        $
 Shdw��64   $  Transparent��6B   #��z! 6V   ��6�   �� *ActualWidth+$" 	Placement��._�5f      �� $# DropDownBorder��6!   #��� ��69   $��1q�6�   #��� ��6�   ._�5g      �� .��5h   "   �� $$ 	Contained=�61   # z 6f   5i      5j      5k      5l        5m      $% Border��6'   #& z��67   #' z��6c   #( z��6�   $��2��6�   #) z 6�   #+ z* 6  �� 5n      $, PART_EditableTextBox��6   #��z��68   #��z��6�   8��  #��z��6)  - 6�   �� *
IsReadOnly+ �� *TemplatedParent+6)  d� 5o      $��1��6   8�� . 6/   �� *IsDropDownOpen+$/ TwoWay=� �� *TemplatedParent+6�   5p       5q      P� 5r       �@J�IsKeyboardFocusWithin . . �5s      ��  � ��
 �   �63   5t      P� 5u       �@��IsDropDownOpen . . �5v      �� $ Border��66    �@ RenderFocused . 6J   5w      P� 5x       �@��HasItems .  . �5y      �� $ DropDownBorder��6/    � ��$ 95��6K   5z      P� 5{       � {� .  . �5|      �� # � ��63    � ���� 5}       � �� �	#FFF4F4F463   5~      P� 5       �@��
IsGrouping . . �5�      ��  � �� .  6F   5�      P� 5�      $0 
PART_Popup��63    �@�HasDropShadow . . �5�      �� $
 Shdw��6/    � ��$ 0,0,5,5q�6A   �� 5�      $
 Shdw��6.    �@ Color$ 	#71000000��6@   5�      5�      �� 5�   
   "
 ��6'   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � ��# � ��6+   �� 5�       � �� �	#FFB8FADA6+   �� 5�       � ��8  6,   �� 5�       � ��$ 1q�60   �� 5�       � ��$
 Auto=�6K   �� 5�       � ��$
 Auto=�6I   �� 5�       � ��$	 4,3q�6(   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � $ MainGrid��6    .  5�      �� 5�   "   $��* �63   �� 5�   "   #��� r�63   $��0 �6�   .+�5�      � $ 
PART_Popup��6$    . $��2��6R   $��1q�6�   #��� .�6�   $��Bottom=�6  ��6f   �� *IsDropDownOpen+ �� *TemplatedParent+.��5�   "     $
 Shdw��6@   $  Transparent��6N   #��z! 6b   ��6�   �� *ActualWidth+$" MainGrid��._�5�   &   �� $# DropDownBorder��6-   #��� ��6E   $��1q�6�   #��� ��6�   ._�5�   *   �� ��. .��5�   .   �� $$ 	Contained=�6=   # z 6r   5�   +   5�   '   5�   #   5�      d� 5�      #��z��6+   #��z��6W   $��2��6�   8�� . 6�   �� *IsDropDownOpen+$/ TwoWay=� �� *TemplatedParent+6�   �� 5�      #��z1 6/   #��z��6l   #��z2 6�   #4 z3 6�   #��z��6  5 .  #��z��6{  # z 6�  #��z��6�  5�       5�      P� 5�      $0 
PART_Popup��6?    �@�HasDropShadow . . �5�   "   �� $
 Shdw��6;    � ��$ 0,0,5,5q�6M   �� 5�   "   $
 Shdw��6:    �@ Color$ 	#71000000��6L   5�      P� 5�       �@��HasItems .  . �5�   "   �� $ DropDownBorder��6;    � ��$ 95��6W   5�      P� 5�       � {� .  . �5�   "   �� # � ��6?    � ���� 5�   "    � �� �	#FFF4F4F46?   5�      P� 5�       �@��
IsGrouping . . �5�   "   ��  � �� .  6R   5�      5�      5�      6 5�      P� 5�       �@��
IsEditable . . �5�      ��  � ��8  64   �� 5�       � ��# � ��63   �� 5�       � �� .  62   �� 5�       � ��$ 3q�60   �� 5�       � ��8  61   5�      5�      5�      .��5�      � .+�5�   
   �� -   5        7 ��Name$7 OKImgBtn��5�      $
��Left=�6"   $	��100��6=   $��745,440,0,0q�6J   $	��Top=�6_   $	��100��6w   $8��2pack://siteoforigin:,,,/Resources/back_Btn_Img.png��6�   �� 5�   
   -   5        8 ��Name$8 SizeNameLbl��5�      $
��Size��6%   $��Center=�64   $��36(�6Q   
����d�$��336,397,456,100q�6{   $��81��6�   �� 5�   
   -   5        $7 LeftSizeBtn��5�      $
��Left=�6%   $��80��6@   $��253,388,0,0q�6L   $	��Top=�6a   $��58��6y   $9��3pack://siteoforigin:,,,/Resources/LeftArrowSize.png��6�   �� 5�   
   -   5        $7 RightSizeBtn��5�      $
��Left=�6&   $��80��6A   $��447,388,0,0q�6M   $	��Top=�6b   $��55��6z   $:��4pack://siteoforigin:,,,/Resources/RightArrowSize.png��6�   �� 5�   
   -   5        $7 UpImgBtn��5�      $
��Left=�6"   $��80��6=   $��
147,37,0,0q�6I   $	��Top=�6]   $��80��6u   $3��-pack://siteoforigin:,,,/Resources/UpArrow.png��6�   $y�Hidden=�6�   �� 5�   
   -   5        $7 RightImgBtn��5�      $
��Left=�6%   $��80��6@   $��232,118,0,0q�6L   $	��Top=�6a   $��80��6y   $6��0pack://siteoforigin:,,,/Resources/rightArrow.png��6�   $y�Hidden=�6�   �� 5�   
   -   5        $7 
DownImgBtn��5�      $
��Left=�6$   $��80��6?   $��147,202,0,-2q�6K   $	��Top=�6a   $��80��6y   $5��/pack://siteoforigin:,,,/Resources/DownArrow.png��6�   $y�Hidden=�6�   �� 5�   
   -   5        $7 LeftImageBtn��5�      $
��Left=�6&   $��80��6A   $��
62,118,0,0q�6M   $	��Top=�6a   $��80��6y   $5��/pack://siteoforigin:,,,/Resources/LeftArrow.png��6�   $y�Hidden=�6�   �� 5�   
   -	   5        $8 SilderNameLbl_Copy��5�      $��Position��6,   $��Center=�6?   $��36(�6\   
����d�$��314,167,407,330q�6�   $	��152��6�   �� 5�   
   -
   5        $7 PrintImgBtn��5�      $
��Left=�6%   $	��190��6@   $��
499,37,0,0q�6M   $	��Top=�6a   $	��170��6y   $5��/pack://siteoforigin:,,,/Resources/PrintIcon.png��6�   6�   �� 5�   
   -   5        $7 PrintCardImgBtn��5�      $
��Left=�6)   $	��150��6D   $��773,150,-173,0q�6Q   $	��Top=�6i   $	��150��6�   $;��5pack://siteoforigin:,,,/Resources/CardMarkingIcon.png��6�   �� 5�   
   -   5        $7 lightImgBtn��5�      $
��Left=�6%   $	��190��6@   $��	44,37,0,0q�6M   $	��Top=�6`   $	��170��6x   $4��./JPTLaserMarkWPF;component/Resources/light.png��6�   6�   5�      5�      