   M S B A M L   `   `   `  ���� L  HKlengPrototype0.3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KlengKleng      Kleng.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Kleng     U QMaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -   $	��640��$	��480��   MaterialDesignPaper#���     MaterialDesignFont#���    � AllowsTransparency  .  � 
ResizeMode$ NoResize=�$��16(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�.��.��� $��0,35,0,0q� � ColumnDefinitions �� $
��277* ��� $
��363* �.+��� -    ��Name$ background_png��$��2��$��	0,-36,0,0q�$+��%Views/Resources/Images/background.png��$
��Fill=�� $��2��$	��614��$��80��$��
10,361,0,0q�$
��Left=�$	��Top=� �� $
��237* ��� $
��377* �.+��� -    ��Name$ KinectModel��$��1��$	��210��$��
161,10,0,0q�$
��Left=�$	��Top=�$��20(�$
��Bold"�$��Microsoft Kinect 1517��$
��Wrap=��� -   $ Version��$��1��$��
295,34,0,0q�$
��Left=�$	��Top=� # MaterialDesignDisplay3TextBlock#��[ $��12(�$
��Bold"�$��Alpha 3.1.14��$
��Wrap=�$��72���� -   $ kinect_status��$��
10,14,96,6q�$3��-Views/Resources/Icons/kinect_disconnected.png��'  !MaterialDesignThemes.Wpf.Snackbar  -   	  Name$	 status��$��2��$	��100��$��169,8,0,-28q�$
��Left=�$	��Top=�
������
   MessageQueue
 4  .MaterialDesignThemes.Wpf.MessageQueueExtension  � $��2��$	��614��$	��161��$��	16,10,0,0q�$
��Left=�$	��Top=� �� $��16 ��� .+��� -   $
 Logo��$��1��$	��184��$	��206��$��169,68,0,-113q�$
��Left=�$	��Top=�$1��+Views/Resources/Images/kleng_ant_shadow.png��#  MaterialDesignThemes.Wpf.Chip  -     Name$ About_Button��$��2��$��-5,-40,0,169q�$��Cristopher Alvear�� � ToolTip$ Autor y Agradecimientos��   Icon �� $ ��Views/Resources/author.png��� $��2��$	��614��$	��180��$��
10,171,0,0q�$
��Left=�$	��Top=�.+��� $��10,0,399,30q�$-��'Views/Resources/Images/reading_card.png��$��UniformToFill=��� -	    ��Name$ Reading_Button��$��0��$��60��$��60��$��
0,0,370,10q�$��Right=�$��Bottom=� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ $ Menú de Lectura��.��'  !MaterialDesignThemes.Wpf.PackIcon  $��30��$��30���  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=null]]   Kind$ Voice=��� -
   $ Reading_Title��$��
76,150,0,0q�$
��Left=�$	��Top=�$��18(�$
��Bold"�$��Lectura��$
��Wrap=��� $��399,0,10,30q�$,��&Views/Resources/Images/terms_union.png��$��UniformToFill=��� -   $ ParingTerms_Button��$��0��$��60��$��60��$��
0,0,190,10q�$��Right=�$��Bottom=�#��[ $! Menú de Términos Pareados��.��  $��30��$��30��$ Marker=��� -   $ PariedTerms_Title��$��435,150,0,0q�$
��Left=�$	��Top=�$��Roboto)�$��18(�$
��Bold"�$��Términos Pareados��$
��Wrap=��� -   $ Close_Button��$��0��$��1��$��29��$��30��$��0,-30,5,445q�$��Right=�$��Bottom=�#��[ .��  $��19��$��20��$ Close=��� -   $ Help_Button��$��0��$��1��$��16��$��16��$��0,-30,40,456q�$��Right=�$��Bottom=�#��[ .��  $��12��$��12��$
 Help=�