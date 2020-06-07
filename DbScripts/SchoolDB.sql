

create table Department (
	Id int Identity(1,1) primary key,
	[Name] nvarchar(255) not null
);

create table Employee (
	Id int identity(1,1) primary key,
	[Name] nvarchar(255) not null,
	DepartmentId int foreign key references Department(Id)
);

GO


insert into Department ([Name]) values
('HR'),
('IT'),
('Delivery');

insert into Employee ([Name], DepartmentId) values
('Alice', 1),
('Bob', 1),
('Tony', 2),
('John', 2),
('Kevin', 3);

select * from Department;
select * from Employee;

select e.name, d.name as DepartmentName from Employee e 
	join Department d on e.DepartmentId = d.id;
