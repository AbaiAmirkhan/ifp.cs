Which parts of your program handle user input and output?

The Main method handles all user interaction, reading inputs via Console.ReadLine() and displaying errors or final results with Console.WriteLine()

Which functions perform only delivery price calculations?

CalculateDeliveryPrice and ApplyRule perform calculation logic

How is Func<...> used to apply delivery pricing rules?

Func<decimal, decimal> variables store lambda functions that represent specific price modifier rules, which are passed into the ApplyRule higher-order function to transform prices 

Why is TryParse useful when processing delivery data entered by the user?

TryParse attempts data conversion without throwing runtime exceptions if the user inputs invalid text, allowing handling through validation and early returns
