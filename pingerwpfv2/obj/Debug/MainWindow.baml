   M S B A M L   `   `   `  ����F  Bpingerwpfv2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:pingerwpfv2pingerwpfv2  5   	       pingerwpfv2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:pingerwpfv2     5   	     � 
ResizeMode$  CanMinimize=�61    � Title$ Ping Tester��5   	   $	��350��6   $	��300��6*   
���   �.��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5       �� $
��Left=�6   $��Center=�6/   
�������.��6]   Runtime (h:m:s)6n   �� 5!      -   5        $��	lDuration��5!   $   $��1��6   $��Center=�65   $��Center=�6R   
�������6m   5#      �� 5%   
   $��1��6   $
��Left=�6   $��Center=�68   
�������.��6f   Ping6l   �� 5&   
   -   5        $��lPing��5&      $��1��6   $��Center=�6*   $��Center=�6G   �� 5(   
   $��2��6   $
��Left=�6   $��Center=�68   
�������.��6f   	Average6o   �� 5)   
   -   5        $��lPingMedian��5)      $��2��6   $��Center=�60   $��Center=�6M   �� 5+   
   $��3��6   $
��Left=�6   $��Center=�68   
�������.��6f   Worst6m   �� 5,   
   -   5        $��
lPingWorst��5,      $��3��6   $��Center=�6/   $��Center=�6L   
���  ��6g   �� 5.   
   $��4��6   $
��Left=�6   $��Center=�68   
�������.��6f   Best6l   �� 5/   
   -   5        $��	lPingBest��5/      $��4��6   $��Center=�6.   $��Center=�6K   
��� � �6f   �� 52   
   $��5��6   $
��Left=�6   $��Center=�68   
�������.��6g   Jumps6n   �� 53   
   -   5        $��
lPingJumps��53      $��5��6   $��Center=�6/   $��Center=�6L   
�������6g   �� 54   
   -   5        $��lPingJumpsMinute��54      $��6��6   $��Center=�65   $��Center=�6R   
�������6m   �� 56   
   $��7��6   $
��Left=�6   $��Center=�68   
�������.��6g   
Highping %6s   �� 57   
   -   5        $��	lPingHigh��57      $��7��6   $��Center=�6.   $��Center=�6K   
�������6f   �� 59   
   $��8��6   $
��Left=�6   $��Center=�68   
�������.��6g   Highping Average6y   �� 5:   
   -	   5        $��lPingHighPingMedian��5:      $��8��6   $��Center=�68   $��Center=�6U   
���  ��6p   �� 5=   
   $��9��6   $
��Left=�6   $��Center=�68   
�������.��6g   
Timeouts6q   �� 5>   
   -
   5        $��lPingTimeouts��5>      $��9��6   $��Center=�62   $��Center=�6O   
�������6j   �� 5@   
   -   5        $��bDocumentation��5@   @   $��10��6   $��Documenting pings: off��6   6V   �� 5B   
   -   5        $��
bCopyBrief��5B   A   $��11��6   $��Copy brief to clipboard��6   6S   5E      5F      