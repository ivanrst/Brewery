# Brewery

This is a brewery system main module. The brewery makes 4 types of beer: Pilsner, Lager, IPA and Stout. It sells beer in barrels and only takes orders. 

The brewery stores its customers' data in this system. Each customer has ID, first name, last name, ordered barrels and delivered barrels. Each ordered barrel has customer, beer type and order date. Each delivered barrel has customer, beer type, order date and delivery date. 

The system registers new customers and takes first name and last name as parameters. It also generates an ID. 

It registers new orders and takes custemer ID, beer type and order date as parameters.

It registers deliveries. If the customer has ordered several barrels of this beer type, and these are not delivered yet. The brewery first delivers the order with the earliest order date. (FIFO principle  - First In First Out). 

It is also able to get customers based on the ID. Then, it returns name and all ordered/delivered barrels.

The brewery wants to know their best customers and has a method which sorts all customers by the number of ordered and delivered barrels.

It also wants to know how many days it takes between an order being placed and its delivery.
