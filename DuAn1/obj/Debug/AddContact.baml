   M S B A M L   `   `   `  ����@  <DuAn1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DuAn1DuAn1  5   	       DuAn1.AddContact   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:DuAn1     5   	     � Title$  
AddContact��5   	   $	��200��6   $	��250��6)    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ local:CustomCommand.Exit��5
      6�   .��5      �  � ColumnDefinitions 5   
   �� 5      $��66 �6   �� 5      $��66 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��	tbContact��5      $��Contact's Name:��6%   $��12(�6<   $��2��6J   $��Center=�6^   $��1q�6y   �� 5   
   -   5        $��tbName��5      $��12(�6    $��1��6.   $��3��6<   $��Center=�6P   $��1q�6k   �� 5   
   -   5        $��tbPhoneNumber��5      $��Phone Number:��6)   $��12(�6>   $��2��6L   $��2��6Z   $��Center=�6n   $��1q�6�   �� 5   
   -   5        $��tbPhone��5      $��12(�6!   $��3��6/   $��3��6=   $��Center=�6Q   $��1q�6l   �� 5    
   -   5        $��tbImagePath��5       $��12(�6'   $��5��65   $��3��6C   $��Center=�6W   �� 5!   
   -   5        $��btSelImg��5!      $��Select Image��6!   $��4��68   $��2��6E   $
��Auto��6Y   6f   c� 5"   
   -   5        $��tbTest��5"      $��40��6     c� Band$ 2��6,   $��6��65   $��3��6B   .@�5#      � .+�5$      �� -	   5        $
��btOk��5$      $��OK��6%   $��12(�62   $��5q�6@   $
��Left=�6K   $��20��6f    �� 	IsDefault . 6�   �� 5%      -
   5        $��btCancel��5%      $��Cancel��6)   $��12(�6:   $��5q�6H   $��Right=�6S   $��20��6o   6{   �� 5&      -   5        $��cpCopy��5&      $
��Copy��6'   $��Center=�66   $��5q�6S   $��Center=�6^   $��ApplicationCommands.Copy��6y   �� 5'      -   5        $��cpPaste��5'      $��Paste��6(   $��Center=�68   $��5q�6U   $��Center=�6`   $��ApplicationCommands.Paste��6{   �� 5(      -   5        $
��Exit��5(      $
��Exit��6%   $��Center=�64   $��5q�6Q   $��Center=�6\   $��local:CustomCommand.Exit��6w   5*      5,      5-      5.      