SELECT OrderRules.Id as 'OrderRuleId', Orders.Id as 'OrderId', Orders.Created_At as 'Created', Products.Name, Product_Price, Amount  FROM Orders
	INNER JOIN OrderRules on Orders.Id = OrderRules.Order_Id
	INNER JOIN Products on OrderRules.Product_Id = Products.Id
	INNER JOIN Staff on Orders.Staff_Id = Staff.Id
	Where SeatedTable_Id = 1