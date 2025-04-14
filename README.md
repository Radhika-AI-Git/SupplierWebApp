Solutions:
1.Suppliers List -This page displays the all the suppliers list.We can add/edit/delete suppliers and also add quotations for particular supplier
2.Quotations List- This page displays the all the quotations list.We can add/edit/delete quotations and also export the quotations list using csv file.
Requirements:
1. Create a new ASP .net core project. User identity or authentication is not required. 
2. Create a local SQL Server database entity called “DbSupplier,” consisting of “Name,” “Email,” 
“CountryCode,” and “DateCreated” using Entity Framework with Code-First.  
“DateCreated” should not store the time, just the date alone. 
“CountryCode” is required, with a maximum of 2 characters. 
3. Create page(s) to Create, Update, and Delete suppliers. You can put all these functions on a single 
or different page, whichever is fine as long as the function is available. 
4. Create the following suppliers using the UI. 

5. Create a page to display existing suppliers, showing the following information: 
a. Name 
b. Email 
c. Country  
d. DateCreated 
Instead of showing CountryCode, map the code to the Country Name based on the following: 
GB 
JP 
United Kingdom 
Japan 
6. The code should sort the results by “Country” in ascending order, then by “DateCreated” in 
descending order, then by “Name” in descending order.  
 
The result should look like this: 
Name Email Country DateCreated 
Supplier5 supplier5@gmail.com Japan 19-Aug-2021 
Supplier2 supplier2@gmail.com Japan 18-Aug-2021 
Supplier4 supplier4@gmail.com United Kingdom 19-Aug-2021 
Supplier3 supplier3@gmail.com United Kingdom 19-Aug-2021 
Supplier1 supplier1@gmail.com United Kingdom 18-Aug-2021 
 
7. Create another database entity called “DbQuotation,” consisting of “SupplierId,” “Product,” and 
“CostPerUnit.” 
 
8. Create page(s) to Create, Update, and Delete quotations. You can put all these functions on a 
single or different page, whichever is fine as long as the function is available. 
 
9. If a supplier is deleted, delete all quotations related to the supplier. 
 
10. Create the following quotations using the UI. 
 
11. Please create a page to display existing quotations, including the supplier name, their products, 
and their costs. If a product does not have CostPerUnit, do not display the product. 
 
12. The code should sort the Supplier Name and the Products in descending order. 
 
The result should look like this: 
Supplier Name Products 
Supplier 3 Blanket: $3 
Supplier 2 Diapers: $2 
Blanket: $3.2 
Supplier 1 Spatula: $1.505 
Skipping Rope: $1.05 
Pacifier: $1 
 
13. Create a link/button to export the above supplier/products list into a CSV file. 
