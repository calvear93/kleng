   M S B A M L   `   `   `  ���� L  HKlengPrototype0.3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KlengKleng      Kleng.Views.ReadingView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Kleng     U QMaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -   $	��640��$	��447��   MaterialDesignPaper#���     MaterialDesignFont#���    � AllowsTransparency  .  � Left$ 64�� � 
ResizeMode$ NoResize=�$��16(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�
 � Top$ 64�� � WindowStartupLocation$ Manual=� � WindowStyle$
 None=�.��.��� $	��447��$	��Top=� � ColumnDefinitions �� $
��110* ��� $
��459* ��� $	��27* ��� $��2* ��� $	��42* �.+��� -   	 ��Name$	 background_png��$��5��$-��'Resources/Images/background_defocus.png��$
��Fill=�� $��5��$	��614��$��32��$��	10,10,0,0q�$
��Left=�$	��Top=�� $��2��$	��162��$	��192��$��
10,180,0,0q�$
��Left=�$	��Top=�.+��� -   
 ��Name$

 load��$	��142��$��40��$��	10,10,0,0q�$
��Left=�$	��Top=� � Cursor$
 Hand�� J� RenderTransformOrigin$ 0.257,0.335&� � ToolTip$ Cargar Lectura��.��'  !MaterialDesignThemes.Wpf.PackIcon  $	��109��$��28���  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=null]]   Kind$ 
FileImport=��� -   $

 stop��$	��142��$��40��$��	10,0,0,10q�$
��Left=�$��Bottom=�$
 Hand��{�.  $ Finalizar Lectura��.��  $	��109��$��30��$
 Stop=��� -   $

 play��$	��142��$��40��$��10,0,0,0q�$
��Left=�$��Center=�$
 Hand��{�.  $ Iniciar Lectura��.��  $	��109��$��30��$
 Play=�� $��2��$	��162��$	��128��$��	10,47,0,0q�$
��Left=�$	��Top=�.+��� -   $
	 logo��$	��161��$��78��$��1,50,0,0q�$��Center=�$	��Top=�$ 
0.51,1.437&�$'��!Resources/Images/kleng_shadow.png��� $��1��$��4��$	��447��$	��381��$��	68,47,0,0q�$
��Left=�$	��Top=�.+�d� -    d�Name$ 
save_audio��$��248,348,0,0q�$
��Left=�$	��Top=�$i cActive para guardar el audio de la lectura junto al reporte. (Activelo antes de cargar la lectura).��.��Switch� $	��427��$	��208��$��	10,81,0,0q�$
��Left=�$	��Top=�.+��� -    ��Name$ reading_label��$��	14,13,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Lectura:���� -	   $ corrects_label��$��	14,63,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Palabras Correctas:���� -
   $ wrongs_label��$��	14,92,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Palabras Incorrectas:���� -   $ modulation_label��$��
14,121,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Modulación:���� -    ��Name$
 name��$��	89,15,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $	 run��$��	73,44,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $ reading��$��	77,86,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$�� ��$
��Wrap=��� -   $ correct_words��$��
163,68,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$	��0/0��$
��Wrap=��� -   $ wrong_words��$��
175,97,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$	��0/0��$
��Wrap=��� -   $ 
modulation��$��112,126,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$	��0.0��$
��Wrap=��� -   $ last_word_label��$��
14,150,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Última Palabra:���� -   $ modulation_Copy��$��137,202,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Wrap=��� -   $ 	last_word��$��137,155,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$��Ninguna��$
��Wrap=��� -   $ reading_name��$��	86,18,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$��Nombre de la lectura��$
��Wrap=�� $	��427��$��40��$��	10,10,0,0q�$
��Left=�$	��Top=�.+��� -   $	 	hourglass��$��32��$��32��$��	238,0,0,8q�$
��Left=�$��Center=�$ 
-2.05,0.55&�$��Resources/Icons/timer.png��$
��Fill=��� -   $
 time��$	��121��$��	285,0,0,6q�$
��Left=�$��Center=�
��� ���$��Roboto)�$��28(�$
��Bold"�$ 	2.102,0.6&�$��00:00:00��$
��Wrap=�$ Cronómetro���� -   $
 
SaveReport��$	��102��$��77��$��312,294,0,0q�$
��Left=�$	��Top=�{�.  .��  $��52��$��57��$ ContentSave=�$  Guardar Reporte de Lectura���� -   $ save_audio_label��$��135,342,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��16(�$
��Bold"�$��Grabar audio:���� -   $ reading_status��$��158,56,0,305q�$
��Left=�$��Center=�
��� d �$��Roboto)�$��16(�$
��Bold"�$ 	2.102,0.6&�$��Esperando Carga.��$
��Wrap=��� -   $ reading_status_label��$��18,56,0,305q�$
��Left=�$��Center=�$��Roboto)�$��16(�$
��Bold"�$ 	2.102,0.6&�$��Estado de Lectura:��$
��Wrap=��� -   $	 language_status��$��45��$��30��$��
24,398,0,0q�$
��Left=�$	��Top=�$#��Resources/Languages/en-US.png���� -   $
 Back_Button��$��0��$��4��$��29��$��30��$��
0,0,10,407q�$��Right=�$��Bottom=� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ .��  $��19��$��20��$ 	ArrowLeft=��� -   $
 Help_Button��$��0��$��2��$��16��$��16��$��	0,0,0,418q�$��Right=�$��Bottom=�#��[ .��  $��12��$��12��$
 Help=�