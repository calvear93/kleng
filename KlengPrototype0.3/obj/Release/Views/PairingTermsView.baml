   M S B A M L   `   `   `  ���� L  HKlengPrototype0.3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KlengKleng  "    Kleng.Views.PairingTermsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Kleng     U QMaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -   $	��640��$	��394��   MaterialDesignPaper#���     MaterialDesignFont#���    � AllowsTransparency  .  � Left$ 64�� � 
ResizeMode$ NoResize=�$��16(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�
 � Top$ 64�� � WindowStartupLocation$ Manual=� � WindowStyle$
 None=�.��.��� $	��384��$	��Top=� � ColumnDefinitions �� $
��110* ��� $
��459* ��� $	��27* ��� $��2* ��� $	��13* ��� $	��29* �.+��� -   	 ��Name$	 background_png��$��6��$��	0,0,0,-10q�$-��'Resources/Images/background_defocus.png��$
��Fill=�� $��6��$	��614��$��32��$��	10,10,0,0q�$
��Left=�$	��Top=�� $��2��$	��162��$	��192��$��
10,180,0,0q�$
��Left=�$	��Top=�.+��� -   
 ��Name$

 load��$	��142��$��40��$��	10,10,0,0q�$
��Left=�$	��Top=� � Cursor$
 Hand�� J� RenderTransformOrigin$ 0.257,0.335&� � ToolTip$ Cargar Términos Pareados��.��'  !MaterialDesignThemes.Wpf.PackIcon  $	��109��$��28���  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=null]]   Kind$ 
FileImport=��� -   $

 stop��$	��142��$��40��$��	10,0,0,10q�$
��Left=�$��Bottom=�$
 Hand��{�.  $" Finalizar Términos Pareados��.��  $	��109��$��30��$
 Stop=��� -   $

 play��$	��142��$��40��$��10,0,0,0q�$
��Left=�$��Center=�$
 Hand��{�.  $  Iniciar Términos Pareados��.��  $	��109��$��30��$
 Play=�� $��2��$	��162��$	��128��$��	10,47,0,0q�$
��Left=�$	��Top=�.+��� -   $
	 logo��$	��161��$��81��$��1,47,0,0q�$��Center=�$	��Top=�$'��!Resources/Images/kleng_shadow.png��� $��1��$��5��$	��447��$	��325��$��	68,47,0,0q�$
��Left=�$	��Top=�.+�� $	��427��$	��162��$��	10,79,0,0q�$
��Left=�$	��Top=�.+��� -    ��Name$ pairing_label��$��	14,13,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Términos Pareados:���� -   $ corrects_label��$��	14,63,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Palabras Correctas:���� -	   $ wrongs_label��$��	14,92,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Palabras Incorrectas:���� -
    ��Name$
 name��$��	89,15,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $	 run��$��	73,44,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $ reading��$��	77,86,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $ correct_pairs��$��
163,68,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$	��0/0��$
��Wrap=��� -   $ wrong_pairs��$��
175,97,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$	��0/0��$
��Wrap=��� -   $ modulation_Copy��$��137,202,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Wrap=��� -   $ pairing_name��$��
171,19,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Wrap=��� $�Nombre de la���� $�
 actividad���� -   $
 
SaveReport��$	��102��$��77��$��318,238,0,0q�$
��Left=�$	��Top=�{�.  .��  $��52��$��57��$ ContentSave=�$+ %Guardar Reporte de Términos Pareados���� -   $ pairing_status��$��232,54,0,251q�$
��Left=�$��Center=�
��� d �$��Roboto)�$��16(�$
��Bold"�$ 	2.102,0.6&�$��Esperando Carga.��$
��Wrap=��� -   $ pairing_status_label��$��10,54,0,251q�$
��Left=�$��Center=�$��Roboto)�$��16(�$
��Bold"�$ 	2.102,0.6&�$
��Wrap=��� $�
Estado de ���� $�Términos Pareados���� $�:��� $	��427��$��40��$��	10,10,0,0q�$
��Left=�$	��Top=�.+��� -   $	 	hourglass��$��32��$��32��$��	238,0,0,8q�$
��Left=�$��Center=�$ 
-2.05,0.55&�$��Resources/Icons/timer.png��$
��Fill=��� -   $
 time��$	��121��$��	285,0,0,6q�$
��Left=�$��Center=�
��� ���$��Roboto)�$��28(�$
��Bold"�$ 	2.102,0.6&�$��00:00:00��$
��Wrap=�$ Cronómetro��d� -    d�Name$ left_handed_mode��$��262,295,0,0q�$
��Left=�$	��Top=�$��Switch��$n hActive para cambiar la mano primaria a la izquierda. (Activelo antes de iniciar los términos pareados).���� -   $ left_handed_mode_label��$��155,289,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Modo Zurdo:���� -   $
 Back_Button��$��0��$��4��$��2��$��29��$��30��$��
0,0,10,344q�$��Right=�$��Bottom=� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ .��  $��19��$��20��$ 	ArrowLeft=��� -   $
 Help_Button��$��0��$��2��$��16��$��16��$��	0,0,0,355q�$��Right=�$��Bottom=�#��[ .��  $��12��$��12��$
 Help=�