# CalculatorR365

Preforms calculation on a list of number.

Specify the action followed by a new line then the list of delimiters followed by a new line then the list of numbers separated by the delimiters. 
If no action is provided then a default action "Add" bill be used
If no delimiters are provided the a default delimiter "," will be used. 

Examples:

Add
Default: 1,3,4
; Delimiter: //;\n1;3,4
*** and ^^ Delimiters: //[***][^^]\n1***2^^6***9^^3

Multiply
Default delimiter: *\n1,3,4
*** Delimiter: *\n//[***]\n1***3***4
* and % delimiters: *\n//[*][%]\n1,3,4


