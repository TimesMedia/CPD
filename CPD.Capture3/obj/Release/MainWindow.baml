   M S B A M L   `   `   `  ���� C  ?CPD.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:CPD.Data;assembly=CPD.DataCPD.Data   CPD.Capture3   CPD.Capture3.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9Data(clr-namespace:CPD.Data;assembly=CPD.Data     -     �Name$  	TopWindow�� � WindowStartupLocation$ Manual=� � WindowState$ 	Maximized=�$	��913��$
��1056�� � 	Resources :  4System.Windows.Controls.Primitives.DataGridRowHeader �� 
TargetType �� Property �� Value �� 
TargetType   ButtonTemplate �� CornerRadius	 �� RadiusX
 �� RadiusY&   System.Windows.Controls.DataGrid �� Triggers P� Property P� Value �� RelativeSource �� Path   CPD.Data.QuestionareDoc  QuestionareDoc  SurveyViewSource �� Source �� Source  ModuleViewSource  ArticleViewSource  QuestionViewSource  AnswerViewSource��%�  '        &	  h     '�� �     7   '  -    &	 �    &	 �    7  &	 �    7  &	     7  &	 +    7  &	 V    7  �� "  .���  � ��
 � �����  � ��$ 30����  � ��
 �  ����  � ��$ 3q��� " ��.R��� $ 8��
����  �$��2q�._�� .+��� 
������$	 10��$
 10���� $��Center=�$��Center=��� " ��.���  � ��$	 120����  � ��$ 10q���  � ��8   �� "   P�  �@=�HasError . . ���  �@�ToolTip ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�  ��  �� *	Survey2+8   ��  �� *FK_Module_Survey2+8   ��  �� *FK_Article_Module+8   ��  �� *FK_Question_Article+8   ��  �� *FK_Answer_Question+8   .���� -    ��Name$ CPDTabControl��$��auto ��$��10q�.@��� $��Initialise and select��.��� -    �Name$ GridInitialise��$��0,0,0,0q�$
��auto�� � RowDefinitions �� $	��320 ��� $	��250���� $	��120 ��� $��40 � � ColumnDefinitions �� $��60 ��� $
��17.6 ��� $
��60.4 ��� $��6 ��� $	��454 ��� $	��188 ��� $	��254 �.+�  -     Name$ SurveyDataGrid��   AutoGenerateColumns .     MinRowHeight$ 30Q�$��0��   SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�$��	0,15,0,15q�$��5������ # [    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ SurveyId��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
SurveyId+4  .System.Windows.Controls.DataGridTemplateColumn  $
 Name��    Width5  /System.Windows.Controls.DataGridLengthConverter$	  200 !   CanUserSort! . "   CellTemplate" �� .R��� $
��Auto��$��3q�# �� TextWrapping$
# Wrap=�.������ $
 Naam�$ �� Mode$$ TwoWay=�% �� NotifyOnValidationError% . & �� UpdateSourceTrigger$& PropertyChanged=�' �� ValidationRules' 5	  /System.Windows.Controls.ExceptionValidationRule	  .��  $ IssueId��$
  auto  �� *	IssueId+  -   expirationDateColumn��$ Expiration date��$	  250 ! . " �� .R�(
  "System.Windows.Controls.DatePicker
  ( 
  SelectedDateFormat$
( Long=�) 
  SelectedDate) �� *ExpirationDate+$& PropertyChanged=��� $��5��$��	10,10,0,0q�.+��� $��RTF file name���� $��Vertical advert URL���� $��Horisontal advert URL���� $��	EBook URL���� $��Accreditation number���� $��CME publication���� $��	CME issue���� $	��URL���� $��
Properties���� -   * ��Name$* ButtonUpdateSurvey��$��3��$��20��.��Update Survey�� -   $* ButtonAddSurvey��$��20��.��
Add Survey�� + � DataContext#+ [ $��6��$��	0,10,10,0q�.+��� $��26������ $ RTFFileName��� $��26������ $ VerticalAdvertURL��� $��26������ $ HorisontalAdvertURL��� $��26������ $ EBookURL��� $��26������ $ AccreditationNumber��� $��26������ $ CMEPublication��� $��26������ $ CMEIssue��� $��26������ $	 URL��� -   , ��Name$, ListFacility��$	��100��- �� SelectedItem- �� $ Facility�$$ TwoWay=�  -	   $ ModuleDataGrid��$��1�� .  .   EnableRowVirtualization. . /   RowDetailsVisibilityMode$/ VisibleWhenSelected=�$
 TrueY� . 0   SelectionUnit$0 FullRow=�$��5������ # [    -
   moduleIdColumn��$ 	Module Id��$  SizeToHeader  �� *
ModuleId+  $ SurveyId�� �� *
SurveyId+  $
 Name��$	  300 ! . " �� .R��� $
��Auto��$��3q�$
# Wrap=�.������ $
 Naam�% . $$ TwoWay=�$& PropertyChanged=�' 	  .���� $��1��$��5��$��10,0,0,0q�.+��� $��Passrate���� $��NormalPoints���� $��Ethics points���� $��
Advert URL���� $��HorisontalAdvert URL���� $��	EBook URL���� -   $* ButtonUpdateModule��$��30��.��Update Module� -   $ 
ModuleGrid��$��1��$��6��#+ [ $��0,0,10,0q�.+��� .+��� $��26������ $ Passrate��� $��26������ $ NormalPoints��� $��26������ $ EthicsPoints��� $��26������ $ 	AdvertURL��� $��26������ $ HorisontalAdvertURL��� $��26������ $ EBookURL�  -   $ ArticleDataGrid�� .  . . $
 TrueY�$��	0,10,10,0q� .  . $/ VisibleWhenSelected=�$ 30Q�$��2��$��7��$0 FullRow=����� # [    $ 	ArticleId�� �� *	ArticleId+  $ ModuleId�� �� *
ModuleId+  -   ArticleColumn��$ Article�� �� *Naam+�� -   $* ButtonUpdateArticle��$��3��$��6��$	��100��$��77,10,77,10q�.��Update Article�� $��Capture��.��� -   $ GridCapture��$��0,0,0,0q�$
��auto�� �� $	��400 ��� $	��200�� �� $	��42* ��� $
��451* ��� $��5 ��� $
��493* �.+��� $��0��$��2��.+�  -   $ QuestionDataGrid��$��0�� .  . . $
 TrueY�$��0,10,0,0q�$/ VisibleWhenSelected=�$ 30Q�$0 FullRow=� . "1   VerticalScrollBarVisibility$1 Visible=�$	��350������ # [    $ 
QuestionId�� �� *
QuestionId+$$ TwoWay=�  $ 	ArticleId�� �� *	ArticleId+$$ TwoWay=�  -   numberWithinArticleColumn��$ Number��$  60  �� *NumberWithinArticle+  $ Question��$  * ! . " �� .R��� $
��Auto��$��3q�$
# Wrap=�.������ $ Question�% . $$ TwoWay=�$& PropertyChanged=�' 	  .���� -   $* ButtonUpdateQuestion��$��20��.��Update Question�� $��1��$��2��.+�  -   $ AnswerDataGrid�� .  . . $��0,10,0,0q� . $0 FullRow=�$ 30Q�$
 TrueY� . $1 Visible=�$	��350������ # [    $ AnswerId�� �� *
AnswerId+  $ 
QuestionId�� �� *
QuestionId+  $ Answer��$  * ! . " �� .R��� $
��Auto��$��3q�$
# Wrap=�.������ $ Answer�% . $$ TwoWay=�$& PropertyChanged=�' 	  .��4  .System.Windows.Controls.DataGridCheckBoxColumn  -   correctColumn��$ Correct��$  60 2   IsThreeState2 .   �� *	Correct+�� -   $* ButtonUpdateAnswer��$��20��.��Update Answer�� $��2��$��2��
���   �3 �� ResizeBehavior$3 BasedOnAlignment=�$��Stretch=�$��5��4 �� ShowsPreview4 . ]� $��3��$��2��.+��� $��Rich text format document��$��2��$
��auto��$��5q�
��� ���.���� -   5 ��Name$5 gRichTextBox��"6 �� VerticalScrollBarVisibility$6 Visible=�