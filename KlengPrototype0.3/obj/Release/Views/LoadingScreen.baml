   M S B A M L   `   `   `  ���� L  HKlengPrototype0.3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Kleng.ViewsKleng.Views      Kleng.Views.LoadingScreen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Kleng.Views     U QMaterialDesignThemes.Wpf, Version=2.2.0.725, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -   $	��640��$	��480��   MaterialDesignFont#���     � AllowsTransparency  .  � 
ResizeMode$ NoResize=�$��16(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�����  �� Opacity$	 0.0Q� �� Color$ White��.��� $��10,10,0,108q� � ColumnDefinitions �� �� $��4* �.+�� -   $��Progress_Bar��$��2��$	��620��$��67��$��	0,285,0,0q�$
��Left=�$	��Top=�	 � Maximum$		 100Q�
 � Minimum$
 0Q��� -    ��Name$
 Logo��$��1��$	��363��$	��342��$��
27,-15,0,0q�$
��Left=�$	��Top=�$��Resources/Images/logo.png��