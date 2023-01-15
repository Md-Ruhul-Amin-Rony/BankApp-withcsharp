# BankApp-withcsharp
This Bank app has 5st sattic user with username and password to login where each user has different amount of accounts on it like(user1 has two accounts- savings account and pension acoount etc), every user after successfully logged in can deposit, withdraw and tranfer money between accounts and even can see the transaction history. One user can log out and at that time the program will not shut down instead the program will continue so that another user(5st static users) can login and use all the the functionality.
I have used Dictionary to create static user as 'key' and Tuple as 'value' for password and making a list of Tuple<string, double> for sub accounts where 'string' will represent sub accounts name and 'double' will represent the amount on each sub account. .
For showing transactions history i have shown 'from account' and 'to account' and 'datetime' also.

I have chossen to  use 'Dictionary' and 'Tuple' to solve this problem because i have learned this two topics newly and found it very powerful and exciting and that is why i tried to experiment with 'Dictionary' and 'Tuple'. Beside this, I tried to not use OOP concept on this simple App. 

But i could easily make this App by using OOP concept also which i did for myself. Where i have made a "User Class" for making static user where i used a 'List<Account> accounts' for making different amount of accounts for each user. Then i have created a "Account Class" and to show the 'Transactions' i have chosen a 'List<Transaction>' property on 'Account class' and then  i made a 'Transaction class' with related properties.
  
  I have tried this same app by using Clean Architecture also. Where i have made diiferent folder for making different class and i have used "Dependency Injektion" and i made it more dynamic.
  
